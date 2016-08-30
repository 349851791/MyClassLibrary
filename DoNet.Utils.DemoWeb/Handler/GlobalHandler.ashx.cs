using DotNet.Utils;
using DotNet.Utils.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.Handler
{
    /// <summary>
    /// GlobalHandler 的摘要说明
    /// </summary>
    public class GlobalHandler : IHttpHandler
    {
        MenusManage menusm = new MenusManage();
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "GetMenuForAccordion":
                    resultStr = GetMenuForAccordion(context);
                    break;
                case "GetMenuForTree":
                    resultStr = GetMenuForTree(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }
        
        /// <summary>
        /// 获取菜单数据给手风琴控件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetMenuForAccordion(HttpContext context)
        {
            return  menusm.GetMenusData();
        }

        /// <summary>
        /// 获取菜单数据给树控件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetMenuForTree(HttpContext context)
        {
            return "";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        public void test()
        { 
        }
    }
}