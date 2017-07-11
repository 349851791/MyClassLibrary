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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{ 
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    public static class DataTableHelper<T> where T : new()
    {
        //调用:List<TDZS_ATTACH> attachList =DataTableHelper<TDZS_ATTACH>.ConvertToModel(attachm.SelectBySQL(sqlStr)).ToList();
        //     TDZS_XGZC flfg = DataTableHelper<TDZS_XGZC>.ConvertToModel(flfgm.SelectBySQL(sqlStr)).ToList().FirstOrDefault();

        /// <summary>
        /// 将datatable转换为List对象集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ConvertToModel(DataTable dt)
        {
            IList<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            if (value.GetType().Name.Equals("Decimal"))
                                pi.SetValue(t, Convert.ToInt32(value), null);
                            else
                                pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }


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
        /// 将DataTable的行放入另一个DataTable中
        /// </summary>
        /// <param name="dt">原来的DataTable</param>
        /// <param name="conditionStr">查询条件</param> 
        /// <returns></returns>
        public static DataTable GetTableByTable(DataTable dt,string conditionStr)
        { 
            DataRow[] drArr = dt.Select(conditionStr);//查询
            DataTable dtNew = dt.Clone();
            for (int i = 0; i < drArr.Length; i++)
            {
                dtNew.ImportRow(drArr[i]);
            }
            return dtNew;
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
