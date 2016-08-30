using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{
    internal class ObjectBase<T> where T : class
    {
        DataBaseLayer dbl = new DataBaseLayer();

        #region 查询

        /// <summary>
        /// 查询返回所有数据,根据实体对象生成条件.
        /// </summary> 
        /// <param name="t">实体类,将查询条件赋给实体类的属性</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>   
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public DataTable Select(T t, string order = "", string columnName = "*")
        {
            string sql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName();
                string strCondition = InternalBase<T>.GetParamForSelectAndDelete(t);
                string strOrder = InternalBase<T>.AddOrder(order);//获取排序字段 
                sql = string.Format("select {0} from {1} {2} {3}", columnName, strTable, strCondition, strOrder);
                return dbl.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, sql);
                return null;
            }
        }

        /// <summary>
        /// 查询显示分页后的列表数据,根据实体对象生成条件.
        /// </summary>
        /// <param name="t">实体类,将查询条件赋给实体类的属性</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable SelectPage(T t, String order, int pageCurrent, int pageSize = 10, string columnName = "*")
        {
            string sql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName();
                string strCondition = InternalBase<T>.GetParamForSelectAndDelete(t);
                string strOrder = InternalBase<T>.AddOrder(order);//获取排序字段
                int number;
                InternalBase<T>.GetPageNumber(pageCurrent, pageSize, out number);//获取分页信息
                string tt, ttt;
                InternalBase<T>.GetColumnName(columnName, out tt, out ttt);
                sql = "SELECT " + columnName + " FROM (SELECT " + ttt + ", ROWNUM AS rowno FROM (  SELECT " + tt + " FROM " + strTable + " t  " + strCondition + strOrder + ") tt  WHERE ROWNUM <= " + (number + pageSize) + ") table_alias WHERE table_alias.rowno >= " + (number + 1);
                return dbl.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, sql);
                return null;
            }
        }

        /// <summary>
        /// 查询显示分页数据.返回json字符串(包含列表数据和总数据量数据,配合easyui使用),根据实体对象生成条件.
        /// </summary>
        /// <param name="t">实体类,将查询条件赋给实体类的属性</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public string SelectPageToJSON(T t, String order, int pageCurrent, int pageSize = 10, string columnName = "*")
        {
            string sql = string.Empty;
            string countSql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName();
                string strCondition = InternalBase<T>.GetParamForSelectAndDelete(t);
                DataTable dt = SelectPage(t, order, pageCurrent, pageSize, columnName);// dbl.ExecuteQuery(sql);
                countSql = "select count (*) from " + strTable + strCondition;
                var pageCount = dbl.GetSingle(countSql);
                return JSONHelper.ObjectToJson(new { total = pageCount, rows = dt });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, sql + "\r\n" + countSql);
                return "";
            }
        }
        #endregion

        #region 增删改

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <returns></returns>
        public int Insert(T t)
        {
            try
            {
                string sql = GetInsertSql(t);
                return dbl.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <param name="conditionName">条件字段,默认为ID,如果有多个条件,可以用","隔开</param>
        /// <returns></returns>
        public int Update(T t, String conditionName = "ID")
        {
            try
            {
                string sql = GetUpdateSql(t, conditionName);
                return dbl.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t">实体类型</param> 
        /// <returns></returns>
        public int Delete(T t)
        {
            try
            {
                String sql = GetDeleteSql(t);
                return dbl.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }
        #endregion

        #region 返回sql语句
        /// <summary>
        /// 获取insert语句
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <returns></returns>
        public string GetInsertSql(T t)
        {
            string strTable = "";//表名
            string strColumn = "";//列
            string strValue = "";//值

            try
            {
                //获取表名
                strTable = InternalBase<T>.GetTableName();
                //获取列名和值
                PropertyInfo[] pi = t.GetType().GetProperties();
                foreach (var item in pi)
                {
                    string key = item.Name;
                    object value = item.GetValue(t, null);
                    if (value == null)
                    {
                        continue;
                    }
                    strColumn += "," + key;//列名 
                    //值
                    if (value.GetType() == typeof(DateTime))
                    {
                        strValue += ",to_date('" + value + "','yyyy/mm/dd hh24:mi:ss')";
                    }
                    else
                    {
                        strValue += ",'" + value + "'";
                    }
                }
                return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", strTable, strColumn.Substring(1), strValue.Substring(1));

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "-1";
            }
        }


        /// <summary>
        /// 获取update语句
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <param name="conditionName">条件字段,默认为ID,如果有多个条件,可以用","隔开</param>
        /// <returns></returns>
        public string GetUpdateSql(T t, string conditionName = "ID")
        {
            try
            {
                string strTable = "";//表名
                string strSetValue = "";//更新的值
                string strCondition = "";//条件

                //获取表名
                strTable = InternalBase<T>.GetTableName();

                PropertyInfo[] pi = t.GetType().GetProperties();
                bool isCondition;//此属性是否为条件
                foreach (var item in pi)
                {
                    //todo:想使用特性,来判断是否把属性赋空值,但是没好用.目前的问题就是,无法把非string类型的值修改为空
                    //string columnType = string.Empty;
                    //object[] propertyAttrs = item.GetCustomAttributes(false);
                    //for (int i = 0; i < propertyAttrs.Length; i++)
                    //{
                    //    object propertyAttr = propertyAttrs[i];
                    //    //获取Column自定义属性中配置的type值(表的列名)
                    //    columnType = GetColumnType(propertyAttr);
                    //}

                    isCondition = false;
                    string key = item.Name;
                    object value = item.GetValue(t, null);
                    if (value == null)//&& string.IsNullOrEmpty(columnType)
                    {
                        continue;
                    }
                    string[] arrayCondition = conditionName.Split(',');
                    foreach (var condition in arrayCondition)
                    {
                        if (key.ToUpper().Equals(condition.ToUpper()))
                        {
                            strCondition += string.Format(" and {0}='{1}'", key, value);
                            isCondition = true;
                            break;
                        }
                    }
                    if (isCondition)
                    {
                        continue;
                    }
                    else
                    {
                        if (value.GetType() == typeof(DateTime))
                        {
                            strSetValue += string.Format(",{0}={1}", key, "to_date('" + value + "','yyyy/mm/dd hh24:mi:ss')");
                        }
                        else
                        {
                            strSetValue += string.Format(",{0}='{1}'", key, value);
                        }
                    }
                }
                if (strCondition.Length > 0)
                {
                    strCondition = InternalBase<T>.IsKeepAndWhere(strCondition, false, true);
                }
                return string.Format("UPDATE {0} SET {1} {2}", strTable, strSetValue.Substring(1), strCondition);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "-1";
            }
        }

        /// <summary>
        /// 获取delete语句
        /// </summary>
        /// <param name="t">实体类型</param> 
        /// <returns></returns>
        public string GetDeleteSql(T t)
        {
            try
            {
                string strTable = "";
                string strCondition = "";
                //获取表名
                strTable = InternalBase<T>.GetTableName();
                //获取条件
                strCondition = InternalBase<T>.GetParamForSelectAndDelete(t);
                return string.Format("delete from {0} {1}", strTable, strCondition);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "-1";
            }
        }
        #endregion

    }
}
