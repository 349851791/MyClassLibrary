using DotNet.Utils.DLL;
using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.Handler
{
    /// <summary>
    /// DepartmentsHandler 的摘要说明
    /// </summary>
    public class DepartmentsHandler : IHttpHandler
    {
        DepartmentsManage dm = new DepartmentsManage();
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "GetAll":
                    resultStr = GetAll(context);
                    break;
                case "Insert":
                    resultStr = Insert(context);
                    break;
                case "Update":
                    resultStr = Update(context);
                    break;
                case "Delete":
                    resultStr = Delete(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        private string GetAll(HttpContext context)
        {
            int page = Convert.ToInt32(context.Request.Params["page"]);
            int rows = Convert.ToInt32(context.Request.Params["rows"]);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string tool_DEPTNAME = context.Request.Params["tool_DEPTNAME"];
            if (!string.IsNullOrEmpty(context.Request.Params["tool_STATUSTIME_Q"]))
            {
                dic.Add("STATUSTIME__Q", Convert.ToDateTime(context.Request.Params["tool_STATUSTIME_Q"]));
            }
            if (!string.IsNullOrEmpty(context.Request.Params["tool_STATUSTIME_Z"]))
            {
                dic.Add("STATUSTIME__Z", Convert.ToDateTime(context.Request.Params["tool_STATUSTIME_Z"]));
            }
            dic.Add("DEPTNAME__like", tool_DEPTNAME);
            return dm.SelectPageToJSON(dic, "ID desc", page, rows);
        }

        private string Insert(HttpContext context)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                ["ID"] = dm.GetSequence(),
                ["DEPTNAME"] = context.Request.Params["DEPTNAME"],
                ["ABBR"] = context.Request.Params["ABBR"]
            };
            return dm.Insert(dic, "Departments").ToString();
        }

        private string Update(HttpContext context)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>() { ["ID"] = context.Request.Params["ID"] };
            Dictionary<string, object> columDic = new Dictionary<string, object>()
            {
                ["DEPTNAME"] = context.Request.Params["DEPTNAME"],
                ["ABBR"] = context.Request.Params["ABBR"]
            };
            return dm.Update(columDic, dic, "Departments").ToString();
        }

        private string Delete(HttpContext context)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ID", context.Request.Params["ID"]);
            return dm.Delete(dic).ToString();
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