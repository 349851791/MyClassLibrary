using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoNet.Utils.DemoWeb.WebForms.ModuleDemo.easyui_多级表头
{
    public partial class export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string aa = Request.RawUrl;
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("content-disposition", "attachment;filename=\"" + DateTime.Now.ToString("yyyyMMdd") + ".xls\"");
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentType = "Application/ms-excel";
            Response.Write("<html>\n<head>\n<meta http-equiv=Content-Type content=\"text/html; charset=gb2312\">");//
            Response.Write("<style type=\"text/css\">\n.pb{font-size:13px;border-collapse:collapse;} " +
                           "\n.pb th{font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;} " +
                           "\n.pb td{border:0.5pt solid windowtext;padding:2px;}\n</style>\n</head>\n");
            Server.ClearError();
            Response.Write("<body>\n" + Request["txtContent"].Replace("undefined", "") + "\n</body>\n</html>");
            Response.Flush();
            Response.End();
        }
    }
}