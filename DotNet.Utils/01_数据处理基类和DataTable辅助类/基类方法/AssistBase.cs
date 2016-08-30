/// <summary> 
/// 类 说 明： CRUDHelper类中辅助方法的具体实现
/// 编 码 人： 张贺
/// 创建日期： 2016-6-1
/// 更新日期： 
/// 更新内容:
/// </summary> 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils 
{
    internal class AssistBase<T> where T : class
    {
        DataBaseLayer dbl = new DataBaseLayer();

        #region 辅助查询方法 
        /// <summary>
        /// 根据标识列的值返回整条数据
        /// </summary> 
        /// <param name="IdentityValue">标识列的值</param>
        /// <returns></returns>
        internal DataTable SelectByIdentity(string IdentityValue)
        {
            string strTable = "";
            string strCondition = "";
            try
            {
                //获取表名
                strTable = InternalBase <T> .GetTableName();
                //获取条件
                PropertyInfo[] pi = typeof(T).GetProperties();
                foreach (var item in pi)
                {
                    var temp = InternalBase<T>.GetColumnAttribute(item);
                    if (temp.Identity)
                    {
                        strCondition = item.Name + "='" + IdentityValue + "'";
                        break;
                    }
                }
                string sql = $"select * from {strTable} where {strCondition}";
                return dbl.ExecuteQuery(sql);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据列以及列的值,返回对应的一条数据
        /// </summary>
        /// <param name="ColumnName">列名</param>
        /// <param name="ColumnValue">列值</param>
        /// <returns></returns>
        internal DataTable SelectByColumn(string ColumnName, string ColumnValue)
        {
            string strTable = InternalBase<T>.GetTableName();
            string strCondition = "";
            try
            {
                strCondition = ColumnName + "='" + ColumnValue + "'";
                string sql = $"select * from {strTable} where {strCondition}";
                return dbl.ExecuteQuery(sql);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取序列
        /// </summary>
        /// <param name="seqName">序列名:如果 序列名=SEQ_实体类名,则可以不传入参数</param>
        /// <returns></returns>
        internal int GetSequence(string seqName = "")
        {
            string strSeqName = "";
            int seq = -1;
            try
            {
                if (seqName == null || seqName.Equals(""))
                {
                    strSeqName = "SEQ_" + typeof(T).Name;
                }
                else
                {
                    strSeqName = seqName;
                }
                string sql = "select " + strSeqName + ".nextval from dual";
                var result = dbl.GetSingle(sql);
                if (result != null)
                {
                    seq = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return seq;
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="columnName">最大值的列名</param>
        /// <param name="t">实体类,传入的值都将当做查询条件参数</param>
        /// <returns></returns>
        internal int GetMax(string columnName, T t = null)
        {
            try
            {
                string strTable = InternalBase<T>.GetTableName();
                string strCondition = InternalBase<T>.GetParamForSelectAndDelete(t);
                string sql = "select max(" + columnName + ") from " + strTable + strCondition;
                return SelectScalar(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="columnName">最大值的列名</param> 
        /// <param name="t">实体类,传入的值都将当做查询条件参数</param>
        /// <returns></returns>
        internal int GetMin(string columnName, T t = null)
        {
            try
            {
                string strTable = InternalBase<T>.GetTableName();
                string strCondition = InternalBase<T>.GetParamForSelectAndDelete(t);
                string sql = "select min(" + columnName + ") from " + strTable + strCondition;
                return SelectScalar(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }

        internal int SelectScalar(string sql)
        {
            try
            {
                object o = dbl.GetSingle(sql);
                if (o != null)
                {

                    return Convert.ToInt32(o);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, sql);
                return -1;
            }
        }
        #endregion
    }
}
