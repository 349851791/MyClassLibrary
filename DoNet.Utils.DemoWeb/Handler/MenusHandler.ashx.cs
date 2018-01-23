using DotNet.Utils.EFModels;
using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.Handler
{
    /// <summary>
    /// MenusHandler 的摘要说明
    /// </summary>
    public class MenusHandler : IHttpHandler
    {

        MenusManage menusm = new MenusManage();
        MenuModelBLL mb = new MenuModelBLL();
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
            int count;
            var data = mb.GetAll(page, rows, out count);
            return DotNet.Utils.JSONHelper.ObjectToJson(new { total = count, rows = data });
        }

        private string Insert(HttpContext context)
        {
            MENUS m = new MENUS();
            m.ID = menusm.GetSequence();
            m.NAME = context.Request.Params["NAME"];
            m.URL = context.Request.Params["URLS"];
            m.ORDERS = Convert.ToInt32(context.Request.Params["ORDERS"]);
            m.FATHERID = Convert.ToInt32(context.Request.Params["FATHERID"]);
            return mb.Add(m).ToString();
        }

        private string Update(HttpContext context)
        {
            MENUS m = new MENUS();
            m.ID = Convert.ToInt32(context.Request.Params["ID"]);
            m.NAME = context.Request.Params["NAME"];
            m.URL = context.Request.Params["URLS"];
            m.ORDERS = Convert.ToInt32(context.Request.Params["ORDERS"]);
            m.FATHERID = Convert.ToInt32(context.Request.Params["FATHERID"]);
            return mb.Update(m).ToString();
        }

        private string Delete(HttpContext context)
        {
            return mb.Delete(Convert.ToInt32(context.Request.Params["ID"])).ToString();
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