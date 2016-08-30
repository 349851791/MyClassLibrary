using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.WebForms.ModuleDemo.上传
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "upload":
                    resultStr = Upload(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        private string Upload(HttpContext context)
        {
            int i=  context.Request.Files.Count;
            return "";
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