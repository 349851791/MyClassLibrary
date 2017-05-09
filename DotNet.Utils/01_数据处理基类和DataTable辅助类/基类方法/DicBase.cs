using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{
    internal class DicBase<T> where T : class
    {
        DataBaseLayer dbl = new DataBaseLayer();

        #region 查询
        /// <summary>
        /// 查询返回所有数据,根据字典生成条件,用于时间条件为时间段时.
        /// </summary> 
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>
        /// <param name="tableName">表名</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable Select(Dictionary<string, object> dic, string order = "", string tableName = "", string columnName = "*")
        {
            string sql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName);
                string strCondition = InternalBase<T>.GetConditionByDIC(dic);
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
        /// 查询显示分页后的列表数据.根据字典生成条件,用于时间条件为时间段时.
        /// </summary>
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable SelectPage(Dictionary<string, object> dic, String order, int pageCurrent, int pageSize = 10, string tableName = "", string columnName = "*")
        {
            string sql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName);
                string strCondition = InternalBase<T>.GetConditionByDIC(dic);
                string strOrder = InternalBase<T>.AddOrder(order);//获取排序字段. 
                int number;
                InternalBase<T>.GetPageNumber(pageCurrent, pageSize, out number); //获取分页信息
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
        /// 查询显示分页数据.返回json字符串(包含列表数据和总数据量数据,配合easyui使用).根据字典生成条件,用于时间条件为时间段时.
        /// </summary>
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public string SelectPageToJSON(Dictionary<string, object> dic, String order, int pageCurrent, int pageSize = 10, string tableName = "", string columnName = "*")
        {
            string sql = string.Empty;
            string countSql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName);
                string strCondition = InternalBase<T>.GetConditionByDIC(dic);
                DataTable dt = SelectPage(dic, order, pageCurrent, pageSize, tableName, columnName);// dbl.ExecuteQuery(sql);
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
        /// 获取插入语句,根据字典.
        /// </summary>
        /// <param name="columnDic">列数据字典,如果列为日期格式,需要增加后缀__date</param> 
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Insert(Dictionary<string, object> columnDic, string tableName = "")
        {
            try
            {
                string sql = GetInsertSql(columnDic, tableName);
                return dbl.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }


        /// <summary>
        /// 更新,条件为字典格式,有些条件为时间段,无法用实体类表示.关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)
        /// </summary> 
        /// <param name="columnDic">列字典</param>
        /// <param name="conditionDic">条件字典</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Update(Dictionary<string, object> columnDic, Dictionary<string, object> conditionDic, string tableName = "")
        {
            try
            {
                string sql = GetUpdateSql(columnDic, conditionDic, tableName);
                return dbl.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }


        /// <summary>
        /// 删除,根据条件字典,用于有时间段的条件
        /// </summary> 
        /// <param name="dic">条件字典</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Delete(Dictionary<string, object> dic, string tableName = "")
        {
            try
            {
                String sql = GetDeleteSql(dic, tableName);
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
        /// 获取查询语句
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="strOrder">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>
        /// <param name="strCondition">条件,需要填写where</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        internal string GetSelectSql(string strTable, int pageCurrent, int pageSize, string strOrder, string strCondition, string columnName)
        {
            string sql = string.Empty;
            try
            {
                int number;
                InternalBase<T>.GetPageNumber(pageCurrent, pageSize, out number);//获取分页信息
                string tt, ttt;
                InternalBase<T>.GetColumnName(columnName, out tt, out ttt);
                sql = "SELECT " + columnName + " FROM (SELECT " + ttt + ", ROWNUM AS rowno FROM (  SELECT " + tt + " FROM " + strTable + " t  " + strCondition + strOrder + ") tt  WHERE ROWNUM <= " + (number + pageSize) + ") table_alias WHERE table_alias.rowno >= " + (number + 1);
                return sql;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex + "sql:" + sql);
                throw;
            }
        }

        /// <summary>
        /// 根据字典,返回分页需要是sql语句
        /// </summary>
        /// <param name="dataSql">返回查询datatable的sql</param>
        /// <param name="countSql">返回查询数量的sql</param>
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public void GetSelectPageSql(out string dataSql, out string countSql, Dictionary<string, object> dic, String order, int pageCurrent, int pageSize = 10, string tableName = "", string columnName = "*")
        {
            dataSql = string.Empty;
            countSql = string.Empty;
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName);
                string strCondition = InternalBase<T>.GetConditionByDIC(dic);
                string strOrder = InternalBase<T>.AddOrder(order);//获取排序字段. 
                int number;
                InternalBase<T>.GetPageNumber(pageCurrent, pageSize, out number); //获取分页信息
                string tt, ttt;
                InternalBase<T>.GetColumnName(columnName, out tt, out ttt);
                dataSql = "SELECT " + columnName + " FROM (SELECT " + ttt + ", ROWNUM AS rowno FROM (  SELECT " + tt + " FROM " + strTable + " t  " + strCondition + strOrder + ") tt  WHERE ROWNUM <= " + (number + pageSize) + ") table_alias WHERE table_alias.rowno >= " + (number + 1);
                countSql = "select count (*) from " + strTable + strCondition;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, dataSql + "\r\n" + countSql); 
            }
        }

        /// <summary>
        /// 获取插入语句,根据字典.
        /// </summary>
        /// <param name="columnDic">列数据字典,如果列为日期格式,需要增加后缀__date</param> 
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string GetInsertSql(Dictionary<string, object> columnDic, string tableName = "")
        {
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName);//获取表名 
                string strColumn = "";//列
                string strValue = "";//值
                foreach (var item in columnDic)
                {
                    string key = item.Key;
                    object value = item.Value;
                    strColumn += "," + key;//列名 
                    if (item.Key.ToLower().Contains("__date"))
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
        /// 获取更新语句,根据字典.条件为字典格式,有些条件为时间段,无法用实体类表示.关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)
        /// </summary>
        /// <param name="columnDic">列数据字典,如果列为日期格式,需要增加后缀__date</param>
        /// <param name="conditionDic">条件字典</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string GetUpdateSql(Dictionary<string, object> columnDic, Dictionary<string, object> conditionDic, string tableName = "")
        {
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName);//获取表名
                string strSetValue = "";//更新的值
                string strCondition = InternalBase<T>.GetConditionByDIC(conditionDic);//条件   
                                                                                      //获取更新的值
                foreach (var item in columnDic)
                {
                    string key = item.Key;
                    object value = item.Value;
                    if (item.Key.ToLower().Contains("__date"))
                    {
                        strSetValue += string.Format(",{0}={1}", key, "to_date('" + value + "','yyyy/mm/dd hh24:mi:ss')");
                    }
                    else
                    {
                        strSetValue += string.Format(",{0}='{1}'", key, value);
                    }
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
        /// 获取delete语句.根据条件字典,用于有时间段的条件
        /// </summary> 
        /// <param name="dic">条件字典</param>
        /// <param name="tableName">表名 </param>
        /// <returns></returns>
        public string GetDeleteSql(Dictionary<string, object> dic, string tableName = "")
        {
            try
            {
                string strTable = InternalBase<T>.GetTableName(tableName); //获取表名
                string strCondition = InternalBase<T>.GetConditionByDIC(dic);//获取条件  
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
