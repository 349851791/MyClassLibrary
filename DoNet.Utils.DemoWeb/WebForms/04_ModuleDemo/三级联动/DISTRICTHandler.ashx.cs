using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.WebForms._04_ModuleDemo.三级联动
{
    /// <summary>
    /// DISTRICT 的摘要说明
    /// </summary>
    public class DISTRICTHandler : IHttpHandler
    {
        DISTRICTManage dm = new DISTRICTManage();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "GetShi":
                    resultStr = GetShi(context);
                    break;
                case "GetQu":
                    resultStr = GetQu(context);
                    break;
                case "GetJie":
                    resultStr = GetJie(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }
        private string GetShi(HttpContext context)
        {
            ////只查询本行政区的数据
            //string orgId = SessionHelp.CurrentAccount.OrgId;
            //string grade = SessionHelp.CurrentAccount.Grade;
            //if (grade.Equals("市级"))
            //{
            //    //如果当前是市级用户,市级下拉框只显示当前市
            //    string sql = "select name from DISTRICT  where code= '" + orgId + "' ";
            //    string orgName = dm.GetOnlyColumnValue(sql);
            //    return dm.GetShi(orgName);
            //}
            //else if (grade.Equals("县级"))
            //{
            //    //如果当前是县级用户,市级下拉框只显示当前市,查询当前县区的所在市
            //    string sql = "select supername from DISTRICT  where code= '" + orgId + "' ";
            //    string orgName = dm.GetOnlyColumnValue(sql);
            //    return dm.GetShi(orgName);
            //}
            //else
            //{
            return dm.GetShi();
            //}
        }

        private string GetQu(HttpContext context)
        {
            //string orgId = SessionHelp.CurrentAccount.OrgId;
            //string grade = SessionHelp.CurrentAccount.Grade;
            //string sql = "select name from DISTRICT  where code= '" + orgId + "' ";
            //string orgName = dm.GetOnlyColumnValue(sql);
            var sName = context.Server.UrlDecode(context.Request.Params["sName"]);

            ////如果当前是县级用户,县区下拉框只显示当前县区
            //if (grade.Equals("县级"))
            //{
            //    return dm.GetQu(sName, orgName);
            //}
            //else
            //{
            return dm.GetQu(sName);
            //}
        }

        private string GetJie(HttpContext context)
        {
            var qName = context.Server.UrlDecode(context.Request.Params["qName"]);
            var sName = context.Server.UrlDecode(context.Request.Params["sName"]);
            return dm.GetJie(qName, sName);
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