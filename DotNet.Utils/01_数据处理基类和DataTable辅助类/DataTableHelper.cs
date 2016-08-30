/// <summary>
/// 使用前提： 在web.config中的appSettings节点,加入StrSql和DbType
/// 类 说 明： 处理datatable的一些方法
/// 编 码 人： 张贺
/// 创建日期： 2015-10-13
/// 更新日期： 
/// 更新内容:
/// </summary> 
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{ 
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    public static class DataTableHelper
    { 

        /// <summary>
        /// 将DataTable的行放入另一个DataTable中
        /// </summary>
        /// <param name="dt">原来的DataTable</param>
        /// <param name="startIndex">起始行</param>
        /// <param name="endIndex">结束行</param>
        /// <returns></returns>
        public static DataTable GetTableByTable(DataTable dt,int startIndex,int endIndex)
        {
            DataTable newDt = new DataTable();
            newDt = dt.Clone();
            for (int i = startIndex; i < endIndex; i++)
            {
                if (i >= dt.Rows.Count)
                {
                    break;
                }
                DataRow newRow = newDt.NewRow();
                newRow.ItemArray = dt.Rows[i].ItemArray;
                newDt.Rows.Add(newRow); 
            }
            return newDt;
        }

        /// <summary>
        /// 给DataTable排序
        /// </summary>
        /// <param name="dt">需要排序的DataTable</param>
        /// <param name="orderStr">排序字段</param>
        /// <returns></returns>
        public static DataTable SortDataTable(DataTable dt, string orderStr)
        {
            DataTable newDt = new DataTable();
            dt.DefaultView.Sort = orderStr;
            newDt = dt.DefaultView.ToTable();
            return newDt;
        }
        
    }
}
