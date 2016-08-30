using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DotNet.Utils;

namespace Test_Oracle_Web
{
    /// <summary>
    /// easyui_datagrid_group 的摘要说明
    /// </summary>
    public class easyui_datagrid_group : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string resultStr = string.Empty;
            string action = context.Request.QueryString["action"];
            switch (action)
            {
                case "GetList":
                    resultStr = GetList(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        private string GetList(HttpContext context)
        {
            var list_footer = new List<object>();
            list_footer.Add(new { xzqhname = "总计:", hf_gd = "999999" });
            return JSONHelper.ObjectToJson(new { total = 10, rows = SetSeed(), footer = list_footer });
        }

        private List<SJZY_HZ> SetSeed()
        {
            List<SJZY_HZ> list = new List<SJZY_HZ>();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                SJZY_HZ s = new SJZY_HZ();
                s.xzqhcode = (i + 1).ToString().PadLeft(2, '0');
                s.xzqhname = "辽" + (i + 1);

                s.xj_zs = r.Next(1, 5);
                s.xj_mj = r.Next(10, 50);
                s.xj_gd = r.Next(1, 10);

                s.hf_zs = r.Next(1, 5);
                s.hf_mj = r.Next(10, 50);
                s.hf_gd = r.Next(1, 10);

                s.wf_zs = r.Next(1, 5);
                s.wf_mj = r.Next(60, 100);
                s.wf_gd = r.Next(11, 50);
                s.wf_jbnt = r.Next(1, 10);

                s.bl1 = (s.wf_zs / (s.xj_zs + s.hf_zs + s.wf_zs)).ToString("p");
                s.bl2 = (s.wf_mj / (s.xj_mj + s.hf_mj + s.wf_mj)).ToString("p");
                s.bl3 = (s.wf_gd / (s.xj_gd + s.hf_gd + s.wf_gd)).ToString("p");
                list.Add(s);
            }
            return list;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 实际占用汇总表
    /// </summary>
    public class SJZY_HZ
    {
        public string xzqhcode { get; set; }
        public string xzqhname { get; set; } 
        public double xj_zs { get; set; }
        public double xj_mj { get; set; }
        public double xj_gd { get; set; }
        public double hf_zs { get; set; }
        public double hf_mj { get; set; }
        public double hf_gd { get; set; }
        public double wf_zs { get; set; }
        public double wf_mj { get; set; }
        public double wf_gd { get; set; }
        public double wf_jbnt { get; set; }
        public string bl1 { get; set; }
        public string bl2 { get; set; }
        public string bl3 { get; set; }
    }

    public class WFYD_HZ
    {
        public string xzqhcode { get; set; }
        public string xzqhname { get; set; }

        public double xj_zs { get; set; }
        public double xj_mj { get; set; }
        public double xj_gd { get; set; }
        public double xj_jbnt { get; set; }

        public double wfpz_zs { get; set; }
        public double wfpz_mj { get; set; }
        public double wfpz_gd { get; set; }
        public double wfpz_jbnt { get; set; }

        public double wf_zs { get; set; }
        public double wf_mj { get; set; }
        public double wf_gd { get; set; }
        public double wf_jbnt { get; set; }
        public string bl1 { get; set; }
        public string bl2 { get; set; }
        public string bl3 { get; set; }
    }
}