using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.WebForms._04_ModuleDemo.WebUploader异步上传控件.Handler
{
    /// <summary>
    /// webuploaderHandler 的摘要说明
    /// </summary>
    public class webuploaderHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resultStr = string.Empty;
            var aa= context.Request.Params["aa"]; 
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                HttpPostedFile file = files[0];
                var str = file.FileName;
            }
            context.Response.Write("完成");
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