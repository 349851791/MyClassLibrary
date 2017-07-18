using DotNet.Utils;
using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.WebForms._03_FrontDemo.时间轴
{
    /// <summary>
    /// timeAxisHandler 的摘要说明
    /// </summary>
    public class timeAxisHandler : IHttpHandler
    {
        EventTimeAxisManage ttm = new EventTimeAxisManage();
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "GetAll":
                    resultStr = GetAll(context);
                    break; 
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        private string GetAll(HttpContext context)
        {
            string str = "select t.*, t.rowid from EVENTTIMEAXIS t order by orders desc";
            DataTable dt = ttm.SelectBySQL(str); 
            return JSONHelper.ObjectToJson(dt);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}