
using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.Handler
{
    /// <summary>
    /// UsersHandler 的摘要说明
    /// </summary>
    public class UsersHandler : IHttpHandler
    {
        UsersManage um = new UsersManage();
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
                case "GetAssist":
                    resultStr = GetAssist(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetAll(HttpContext context)
        {
            var tool_USERNAME = context.Request.Params["tool_USERNAME"];
            var tool_DUTY = context.Request.Params["tool_DUTY"];
            int page = Convert.ToInt32(context.Request.Params["page"]);
            int rows = Convert.ToInt32(context.Request.Params["rows"]);
            Users users = new Users() { USERNAME = tool_USERNAME, DUTY = tool_DUTY };
            return um.SelectPageToJSON(users, "ID desc", page, rows);
        }

        private string Insert(HttpContext context)
        {
            Users user = new Users()
            {
                ID = um.GetSequence(),
                USERNAME = context.Request.Params["USERNAME"],
                DUTY = context.Request.Params["DUTY"],
                SEX = context.Request.Params["SEX"],
                LOGONID = context.Request.Params["SEX"]
            };
            return um.Insert(user).ToString();
        }

        private string Update(HttpContext context)
        {
            Users user = new Users()
            {
                ID = Convert.ToInt32(context.Request.Params["ID"]),
                USERNAME = context.Request.Params["USERNAME"],
                DUTY = context.Request.Params["DUTY"],
                SEX = context.Request.Params["SEX"],
                LOGONID = context.Request.Params["SEX"]
            };
            return um.Update(user).ToString();
        }

        private string Delete(HttpContext context)
        {
            Users user = new Users() { ID = Convert.ToInt32(context.Request.Params["ID"]) };
            return um.Delete(user).ToString();
        }
        
        private string GetAssist(HttpContext context)
        {
            string identity = context.Request.Params["identity"];
            string columnName = context.Request.Params["columnName"];
            string columnValue = context.Request.Params["columnValue"];
            DataTable dt_identity = um.SelectByIdentity(identity);
            DataTable dt_column = um.SelectByColumn(columnName, columnValue);
            int max = um.GetMax("ID");
            int max_condition = um.GetMax("ID", new Users() { SEX = "男" });
            int min = um.GetMin("ID");
            int min_condition = um.GetMin("ID", new Users() { SEX = "男" });
            var data = new
            {
                dt_identity = dt_identity,
                dt_column = dt_column,
                max = max,
                max_condition = max_condition,
                min = min,
                min_condition = min_condition
            };
            return DotNet.Utils.JSONHelper.ObjectToJson(data);
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