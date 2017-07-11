using System;
using System.Data;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 行政区划
    /// </summary>
    public class DISTRICT
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
        /// 类别
        /// </summary>
        public int? CLASSNO { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int? GRADENO { get; set; }

        /// <summary>
        /// 公文交互时发动到办公室负责人的手机号码
        /// </summary>
        public string BGS_TEL { get; set; }

        /// <summary>
        /// 上一级行政区代码
        /// </summary>
        public string SUPERCODE { get; set; }

        /// <summary>
        /// 行政级别
        /// </summary>
        public int? ADLEVEL { get; set; }

        /// <summary>
        /// 行政区简介
        /// </summary>
        public string DISTRICTDISC { get; set; }

        /// <summary>
        /// 交通位置图
        /// </summary>
        public string TRAFFIC { get; set; }

        /// <summary>
        /// 地理概况图
        /// </summary>
        public string GEOGRAPHY { get; set; }

        /// <summary>
        /// 地质概况图
        /// </summary>
        public string GEOLOGIC { get; set; }

        /// <summary>
        /// 灾害概况图
        /// </summary>
        public string DISASTER { get; set; }

        /// <summary>
        /// 数据交换标识
        /// </summary>
        public int? JHBS { get; set; }

        /// <summary>
        /// 同步ID
        /// </summary>
        public int? SYN_ID { get; set; }

        /// <summary>
        /// 设市区上报报表行政区级别
        /// </summary>
        public string TJGRADE { get; set; }

    }
    public class DISTRICTManage : CRUDHelper<DISTRICT>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetShi(string shiName = "")
        {
            try
            {
                string sql = "select name from DISTRICT where supername='辽宁省' ";
                if (!shiName.Equals(""))
                {
                    sql += " and name='" + shiName + "' ";
                }
                sql += " order by orderno";
                DataTable dt = this.SelectBySQL(sql);
                return JSONHelper.ObjectToJson(dt).Replace("NAME", "TEXT");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex); return "";
            }
        }

        /// <summary>
        /// 根据市级名称返回区县名
        /// </summary>
        /// <param name="sName">市级名称</param>
        /// <returns></returns>
        public string GetQu(string sName, string qxName = "")
        {
            string sql = "select id, name from DISTRICT where supername='" + sName + "' and name!='市辖区'";
            if (!qxName.Equals(""))
            {
                sql += " and name='" + qxName + "' ";
            }
            sql += " order by code";

            return JSONHelper.ObjectToJson(this.SelectBySQL(sql)).Replace("NAME", "TEXT");
        }

        /// <summary>
        /// 根据区县名称返回街道
        /// </summary>
        /// <param name="qName">区级名称</param>
        /// <returns></returns>
        public string GetJie(string qName, string sName)
        {
            string sql = "select name from TLW_DISTRICT_TLEVEL t where supername='" + qName + "' and gname='" + sName + "' order by id";
            // string sql = "select name from DISTRICT where supername='" + qName + "' order by id";
            return JSONHelper.ObjectToJson(this.SelectBySQL(sql)).Replace("NAME", "TEXT");
        }

    }
}

