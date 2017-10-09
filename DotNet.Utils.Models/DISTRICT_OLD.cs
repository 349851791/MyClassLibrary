using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 新区对照表
    /// </summary>
    public class DISTRICT_OLD
    {
        /// <summary>
        /// 标识列
        /// </summary>
        [Column(Identity = true)]
        public int? ID { get; set; }

        /// <summary>
        /// 行政区代码
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 行政区名称
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 上级区域
        /// </summary>
        public string SUPERNAME { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public int? SUPERID { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int? ORDERNO { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string GRADE { get; set; }


        /// <summary>
        /// 级别
        /// </summary>
        public string state { get; set; }


        /// <summary>
        /// 子数据集合
        /// </summary>
        public List<DISTRICT_OLD> children { get; set; }

    }
    public class DISTRICT_OLDManage : CRUDHelper<DISTRICT_OLD>
    {
        public string GetContrastData()
        {
            return JSONHelper.ObjectToJson(GetTreeData(0));
        }

        private List<DISTRICT_OLD> GetTreeData(int id)
        {
            List<DISTRICT_OLD> list = new List<DISTRICT_OLD>();
            string str = "select * from DISTRICT_CONTRAST where SUPERID=" + id + " order by ORDERNO";
            try
            {
                DataTable dt = this.SelectBySQL(str);
                foreach (DataRow dr in dt.Rows)
                {
                    DISTRICT_OLD district = new DISTRICT_OLD();
                    district.ID = Convert.ToInt32(dr["ID"]);
                    district.SUPERID = Convert.ToInt32(dr["SUPERID"]);
                    district.NAME = dr["NAME"].ToString();
                    //district.state = "closed";
                    district.state = dr["state"].ToString();

                    district.ORDERNO = dr["ORDERNO"] == DBNull.Value ? 1 : Convert.ToInt32(dr["ORDERNO"]);
                    district.children = GetTreeData(Convert.ToInt32(dr["ID"]));
                    list.Add(district);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }

            return list;
        }

    }
}

