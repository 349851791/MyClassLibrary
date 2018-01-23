//GetConditionByDIC方法 增加条件 in  no  notin   update by 20170513

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{
    internal class InternalBase<T> where T : class
    { 

        #region 内部调用方法

        /// <summary>
        /// 获取分页页的信息
        /// </summary>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="number">算出来的页信息</param>
        internal static void GetPageNumber(int pageCurrent, int pageSize, out int number)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            if (pageCurrent == 0)
            {
                pageCurrent = 1;
            }
            number = (pageCurrent - 1) * pageSize;
        }

        /// <summary>
        /// 根据实体对象返回条件语句,用于Select和Delete的语句
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <param name="isAnd">是否包含and,默认不包含</param>
        /// <param name="isWhere">是否包含where,默认不包含</param>
        /// <returns></returns>
        internal static string GetParamForSelectAndDelete(T t, bool isAnd = false, bool isWhere = true)
        {
            string strCondition = "";
            try
            {
                if (t != null)
                {
                    PropertyInfo[] pi = t.GetType().GetProperties();
                    foreach (var item in pi)
                    {
                        string key = item.Name;
                        object value = item.GetValue(t, null);
                        if (value == null || value.Equals(""))//20160325 update by zhh加入空字符判断
                        {
                            continue;
                        }
                        if (value.GetType() == typeof(DateTime))
                        {
                            strCondition += string.Format(" and {0}={1}", key, "to_date('" + value + "','yyyy/mm/dd hh24:mi:ss')");
                        }
                        else if (value.GetType() == typeof(String))
                        {
                            strCondition += string.Format(" and {0} like '%{1}%'", key, value);
                        }
                        else
                        {
                            strCondition += string.Format(" and {0} = '{1}'", key, value);
                        }
                    }
                    strCondition = IsKeepAndWhere(strCondition, isAnd, isWhere);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return strCondition;
        }


        /// <summary>
        /// 根据实体类名称或者配置的实体类属性获取数据库表名
        /// </summary>
        /// <returns></returns>
        internal static string GetTableName(string tableName = "")
        {
            try
            {
                if (tableName != "" && tableName != null)
                {
                    return tableName;
                }
                object[] o = typeof(T).GetCustomAttributes(false);
                if (o.Length > 0)
                {
                    object classAttr = o[0];
                    if (classAttr is TableAttribute)
                    {
                        return (classAttr as TableAttribute).Name;
                    }
                    else
                    {
                        return typeof(T).Name;
                    }
                }
                else
                {
                    return typeof(T).Name;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("实体类:" + typeof(T).FullName + "的属性配置[Table(name=\"tablename\")]错误或未配置,采用实体类名当做表名查询");
                LogHelper.WriteLog(ex);
                return typeof(T).Name;
            }
        }

        /// <summary>
        /// 返回实体类各个字段的自定义属性
        /// </summary>
        /// <param name="attribute">属性字段</param>
        /// <returns></returns>
        internal static ColumnAttribute GetColumnAttribute(PropertyInfo attribute)
        {
            var a = (attribute.GetCustomAttributes(typeof(ColumnAttribute), true)[0] as ColumnAttribute).Identity;
            string columnType = string.Empty;
            object[] array = attribute.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (array.Length == 1)
            {
                return (array[0] as ColumnAttribute);
            }
            else {
                return null;
            }
        }


        /// <summary>
        /// 根据条件字典返回查询语句,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like),数据库字段名__no(为不等于),数据库字段名__in(为包含,value中需要包含"()"),数据库字段名__notin(为不包含)
        /// </summary>
        /// <param name="dicCondition">条件字典</param> 
        /// <param name="isAnd">是否返回and,默认为false</param>
        /// <param name="isWhere">是否返回where,默认为true</param>
        /// <returns></returns>
        internal static string GetConditionByDIC(Dictionary<string, object> dicCondition, bool isAnd = false, bool isWhere = true)
        {
            string strCondition = string.Empty;
            try
            {
                foreach (var item in dicCondition)
                {
                    if (item.Value != null && item.Value.ToString() != "")
                    {
                        string operators = "and";//操作符
                        if (item.Key.ToLower().Contains("__or"))
                        {
                            operators = " or";
                        }

                        int marksIndex = item.Key.IndexOf("__");//标记符
                        string key = item.Key;//列名
                        if (marksIndex != -1)
                        {
                            key = item.Key.Substring(0, item.Key.IndexOf("__"));
                        }

                        ////左括号
                        //if (item.Key.ToLower().Contains("("))
                        //{
                        //    strCondition +="("; 
                        //}

                        //是空
                        if (item.Value.ToString().ToLower().Contains("isnull"))
                        {
                            strCondition += string.Format(" {0} {1} is null", operators, key);
                            continue;
                        }

                        //时间段
                        if (item.Key.ToLower().Contains("__q"))
                        {
                            if (item.Value.GetType() == typeof(DateTime))
                            {
                                strCondition += string.Format(" {0} {1} >= to_date('{2}','yyyy/mm/dd hh24:mi:ss') ", operators, key, item.Value);
                            }
                            else
                            {
                                strCondition += string.Format(" {0} {1} >= '{2}'", operators, key, item.Value);
                            }
                            continue;
                        }
                        if (item.Key.ToLower().Contains("__z"))
                        {
                            if (item.Value.GetType() == typeof(DateTime))
                            {
                                strCondition += string.Format(" {0} {1} <= to_date('{2}','yyyy/mm/dd hh24:mi:ss') ", operators, key, item.Value);
                            }
                            else
                            {
                                strCondition += string.Format(" {0} {1} <= '{2}'", operators, key, item.Value);
                            }
                            continue;
                        }
                        //判断大于小于
                        //大于等于
                        if (item.Key.ToLower().Contains("__>="))
                        {
                            strCondition += string.Format(" {0} {1} >= {2}", operators, key, item.Value);
                            continue;
                        }
                        //大于
                        if (item.Key.ToLower().Contains("__>"))
                        {
                            strCondition += string.Format(" {0} {1} > {2}", operators, key, item.Value); //$" {operators} {key} > {item.Value}";
                            continue;
                        }

                        //小于等于
                        if (item.Key.ToLower().Contains("__<="))
                        {
                            strCondition += string.Format(" {0} {1} <= {2}", operators, key, item.Value);// $" {operators} {key} <= {item.Value}";
                            continue;
                        }
                        //小于
                        if (item.Key.ToLower().Contains("__<"))
                        {
                            strCondition += string.Format(" {0} {1} < {2}", operators, key, item.Value);// $" {operators} {key} < {item.Value}";
                            continue;
                        }

                        //判断是否是in
                        if (item.Key.ToLower().Contains("__in"))
                        {
                            strCondition += string.Format(" {0} {1} in {2}", operators, key, item.Value); //$" {operators} {key} in ({item.Value})";
                            continue;
                        }
                        //判断是否是notin
                        if (item.Key.ToLower().Contains("__notin"))
                        {
                            strCondition += string.Format(" {0} {1} not in {2}", operators, key, item.Value);// $" {operators} {key} not in ({item.Value})";
                            continue;
                        }

                        //判断是否是no
                        if (item.Key.ToLower().Contains("__no"))
                        {
                            strCondition += string.Format(" {0} {1} != '{2}'", operators, key, item.Value);// $" {operators} {key} != '{item.Value}'";
                            continue;
                        }

                        //判断是否执行oracle方法
                        if (item.Key.ToLower().Equals("__function"))
                        {
                            strCondition += operators + " " + item.Value;// $" {operators} {item.Value}";
                            continue;
                        }

                        //判断是否是like
                        if (item.Key.ToLower().Contains("__like"))
                        {
                            strCondition += string.Format(" {0} {1} like '%{2}%'", operators, key, item.Value);// $" {operators} {key} like '%{item.Value}%'";
                        }
                        else if (item.Key.ToLower().Contains("__startlike"))
                        {
                            strCondition += string.Format(" {0} {1} like '%{2}'", operators, key, item.Value);// $" {operators} {key} like '%{item.Value}'";
                        }
                        else if (item.Key.ToLower().Contains("__endlike"))
                        {
                            strCondition += string.Format(" {0} {1} like '{2}%'", operators, key, item.Value);// $" {operators} {key} like '{item.Value}%'";
                        }
                        else
                        {
                            strCondition += string.Format(" {0} {1} = '{2}'", operators, key, item.Value); //$" {operators} {key} = '{item.Value}'";
                        }

                        ////右括号
                        //if (item.Key.ToLower().Contains(")"))
                        //{
                        //    strCondition += ")";
                        //}
                    }
                }
                strCondition = IsKeepAndWhere(strCondition, isAnd, isWhere);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return strCondition;
        }

        /// <summary>
        /// 判断条件字符串是否保留and和where
        /// </summary>
        /// <param name="strCcondition">条件字符串</param>
        /// <param name="isAnd">是否保留and</param>
        /// <param name="isWhere">是否保留where</param>
        /// <returns></returns>
        internal static string IsKeepAndWhere(string strCcondition, bool isAnd, bool isWhere)
        {
            try
            {
                if (strCcondition.Length > 0)
                {
                    if (!isAnd)
                    {
                        strCcondition = strCcondition.Substring(4);
                    }
                    if (isWhere)
                    {
                        strCcondition = " where " + strCcondition;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return strCcondition;
        }

        /// <summary>
        /// 增加排序
        /// </summary>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        internal static string AddOrder(String strOrder)
        {
            try
            {
                if (strOrder == null || strOrder.Equals(""))
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = " order by " + strOrder;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return strOrder;
        }

        /// <summary>
        /// 分页时,通过columnName,获取查询语句的t和tt列名
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tt"></param>
        /// <param name="ttt"></param>
        internal static void GetColumnName(string columnName, out string tt, out string ttt)
        {
            tt = "t.*";
            ttt = "tt.*";
            if (!columnName.Equals("*"))
            {
                tt = "";
                ttt = "";
                var arrayColumn = columnName.Split(',');
                foreach (var item in arrayColumn)
                {
                    tt += ",t." + item;
                    ttt += ",tt." + item;
                }
                tt = tt.Substring(1);
                ttt = ttt.Substring(1);
            }
        }
        #endregion
    }
}
