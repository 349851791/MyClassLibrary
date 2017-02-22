using DotNet.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Test_Oracle_Web.NPOI处理excel
{
    /// <summary>
    /// NPOI_DEMO 的摘要说明
    /// </summary>
    public class NPOI_DEMO : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string resultStr = string.Empty;
            string filePath = context.Request.Params["filePath"];
            //var data = new { rows = ImportExcelFile(filePath), total = 32 };
            resultStr = JSONHelper.ObjectToJson(ImportExcelFile(filePath));
            context.Response.Write(resultStr);
        }

        public List<ExcelModel> ImportExcelFile(string filePath)
        {
            HSSFWorkbook hssfworkbook;
            #region//初始化信息
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }

            #endregion

                ISheet sheet = hssfworkbook.GetSheetAt(0);
                ////取行Excel的最大行数
                //int rowsCount = sheet.PhysicalNumberOfRows;

                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                List<ExcelModel> list = new List<ExcelModel>();
                for (int i = 5; i < sheet.PhysicalNumberOfRows-1; i++)
                {
                    ExcelModel em = new ExcelModel();
                    em.GUID = Guid.NewGuid().ToString();
                    em.WPHCID = -1;
                    em.XH = Convert.ToInt32(sheet.GetRow(i).GetCell(0).ToString());
                    em.XZQDM = sheet.GetRow(i).GetCell(1).ToString();
                    em.JCTBH = Convert.ToInt32(sheet.GetRow(i).GetCell(2).ToString());
                    em.TBLX = Convert.ToInt32(sheet.GetRow(i).GetCell(3).ToString()); ;
                    em.XZB = Convert.ToDouble(sheet.GetRow(i).GetCell(4).ToString());
                    em.YZB = Convert.ToDouble(sheet.GetRow(i).GetCell(5).ToString());
                    em.SXQ = Convert.ToDouble(sheet.GetRow(i).GetCell(6).ToString());
                    em.SXH = Convert.ToDouble(sheet.GetRow(i).GetCell(7).ToString());
                    em.JCMJ = Convert.ToDouble(sheet.GetRow(i).GetCell(8).ToString());
                    em.BGHDL = sheet.GetRow(i).GetCell(9).ToString();
                    em.BGFWQK = sheet.GetRow(i).GetCell(10).ToString();
                    em.WBGYY = sheet.GetRow(i).GetCell(11).ToString();
                    em.BZ = sheet.GetRow(i).GetCell(12).ToString();
                    var xzq = sheet.GetRow(1).GetCell(0).ToString();

                    em.SHENGMC = xzq.Substring(0, xzq.IndexOf("省")).Trim();
                    int shiEnd = xzq.LastIndexOf("市");
                    int shiStart = -1;
                    shiStart = xzq.IndexOf(")");
                    if (shiStart == -1)
                    {
                        shiStart = xzq.IndexOf("）")+1;
                    }
                    em.SHIMC = xzq.Substring(shiStart, shiEnd - shiStart).Trim();
                    em.XQMC = xzq.Substring(shiEnd+1).Trim();
                    list.Add(em);
                } 
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class ExcelModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// 卫片核查的外键
        /// </summary>
        public int? WPHCID { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        public string SHENGMC { get; set; }

        /// <summary>
        /// 市名称
        /// </summary>
        public string SHIMC { get; set; }

        /// <summary>
        /// 县区名称
        /// </summary>
        public string XQMC { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int? XH { get; set; }

        /// <summary>
        /// 行政代码
        /// </summary>
        public string XZQDM { get; set; }

        /// <summary>
        /// 监测图斑号
        /// </summary>
        public int? JCTBH { get; set; }

        /// <summary>
        /// 图斑类型
        /// </summary>
        public int? TBLX { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public double? XZB { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double? YZB { get; set; }

        /// <summary>
        /// 时相前
        /// </summary>
        public double? SXQ { get; set; }

        /// <summary>
        /// 时相后
        /// </summary>
        public double? SXH { get; set; }

        /// <summary>
        /// 监测面积
        /// </summary>
        public double? JCMJ { get; set; }

        /// <summary>
        /// 变更后地类
        /// </summary>
        public string BGHDL { get; set; }

        /// <summary>
        /// 变更范围情况
        /// </summary>
        public string BGFWQK { get; set; }

        /// <summary>
        /// 未变更原因
        /// </summary>
        public string WBGYY { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }



    }
}