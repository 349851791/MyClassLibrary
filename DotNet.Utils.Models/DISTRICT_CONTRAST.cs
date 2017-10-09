using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 新区对照表
    /// </summary>
    public class DISTRICT_CONTRAST
    {
        //public DISTRICT_CONTRAST()
        //{
        //}
        //public DISTRICT_CONTRAST(string isEdit)
        //{
        //    this.iconCls = "icon-delete";
        //}
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
        /// 树节点默认是否打开
        /// </summary>
        public string state { get; set; }


        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls { get; set; }

        /// <summary>
        /// 左侧树ID
        /// </summary>
        public int? OLDID { get; set; }
        /// <summary>
        /// 子数据集合
        /// </summary>
        public List<DISTRICT_CONTRAST> children { get; set; }

    }
    public class DISTRICT_CONTRASTManage : CRUDHelper<DISTRICT_CONTRAST>
    {
        public string GetDISTRICTData(int startId, string tableName)
        {
            return JSONHelper.ObjectToJson(GetTreeData(startId, tableName));
        }

        public string GetDISTRICTDataForEdit(int id)
        {
            string sql = "";
            string sql_count = "select count(*) from DISTRICT_CONTRAST where superid=" + id;
            int count = this.SelectScalar(sql_count);
            if (count > 0)
            {
                sql = "select * from DISTRICT_CONTRAST where id=" + id + " order by id";
            }
            else
            {
                string sql_object = "select superid from DISTRICT_CONTRAST t where id=" + id;
                string superid = this.GetOnlyColumnValue(sql_object);
                sql = "select * from DISTRICT_CONTRAST where id=" + superid + " order by id";
            }
            try
            {
                DataTable dt = this.SelectBySQL(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<DISTRICT_CONTRAST> list = new List<DISTRICT_CONTRAST>();
                    DISTRICT_CONTRAST district = new DISTRICT_CONTRAST();
                    district.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    district.SUPERID = Convert.ToInt32(dt.Rows[0]["SUPERID"]);
                    district.NAME = dt.Rows[0]["NAME"].ToString();
                    district.state = "open"; 
                    district.children = GetTreeData(Convert.ToInt32(dt.Rows[0]["ID"]), "DISTRICT_CONTRAST", true);
                    list.Add(district);
                    return JSONHelper.ObjectToJson(list);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return "";
        }

        private List<DISTRICT_CONTRAST> GetTreeData(int id, string tableName, bool isEdit = false)
        {
            List<DISTRICT_CONTRAST> list = new List<DISTRICT_CONTRAST>();
            string sql = "select * from " + tableName + " where SUPERID=" + id + " order by id";
            try
            {
                DataTable dt = this.SelectBySQL(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    DISTRICT_CONTRAST district = new DISTRICT_CONTRAST();
                    district.ID = Convert.ToInt32(dr["ID"]);
                    district.SUPERID = Convert.ToInt32(dr["SUPERID"]);
                    district.NAME = dr["NAME"].ToString();
                    district.state = dr["state"].ToString();
                    if (isEdit)
                    {
                        district.iconCls = "icon-delete";
                        district.OLDID = dt.Rows[0]["OLDID"] == DBNull.Value ? 1 : Convert.ToInt32(dr["OLDID"]);
                    }
                    //district.state = "closed";
                    //district.ORDERNO = dr["ORDERNO"] == DBNull.Value ? 1 : Convert.ToInt32(dr["ORDERNO"]);
                    district.children = GetTreeData(Convert.ToInt32(dr["ID"]), tableName);
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

