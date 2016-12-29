using DotNet.Utils;
using DotNet.Utils.DLL;
using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.WebForms.UtilsDemo
{
    /// <summary>
    /// UtilsDemoHandler 的摘要说明
    /// </summary>
    public class UtilsDemoHandler : IHttpHandler
    {
        UsersManage um = new UsersManage();
        public void ProcessRequest(HttpContext context)
        { 
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "JsonDemo":
                    resultStr = JsonDemo(context);
                    break;
                case "EnumDemo":
                    resultStr = EnumDemo(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }


        private string JsonDemo(HttpContext context)
        {
            //对象转JSON
            Users u1 = new Users() { ID = 9999, USERNAME = "测试人员", DUTY = "科长", SEX = "男" };
            string result1 = JSONHelper.ObjectToJson(u1);
            //JSON转对象
            string str2 = "{'Id':9999,'USERNAME':'测试人员','DUTY': '科长','SEX':'男'}";
            Users u2 = JSONHelper.JsonToObject<Users>(str2);
            //集合转JSON
            List<Users> list3 = new List<Users>() {
                new Users() {ID = 1111, USERNAME = "测试人员1", DUTY = "科长", SEX = "男" },
                new Users() {ID = 8888, USERNAME = "测试人员2", DUTY = "副科长", SEX = "女" }
            };
            var Data3 = new { rows = list3.Count, List = list3 };
            string result3 = JSONHelper.ObjectToJson(Data3);
            //JSON转集合
            string str4 = "[{'Id':9999,'USERNAME':'测试人员1','DUTY': '科长','SEX':'男'},{'Id':8888,'USERNAME':'测试人员2','DUTY': '副科长','SEX':'女'}]";
            List<Users> list4 = JSONHelper.JsonToObject<List<Users>>(str4);
            //DataTable转JSON
            DataTable dt5 = um.Select(new Users() { SEX = "男", DUTY = "科长" });
            string result5 = JSONHelper.ObjectToJson(dt5);
            //JSON转DataTable
            string str6 = "[{\"ID\":6628999.0,\"DEPTID\":1975999.0,\"USERNAME\":\"丁军\",\"PASSWORD\":\"3C87E7540153D879\",\"LOGONID\":\"dingj\",\"DUTY\":\"副科长\",\"SEX\":\"男\",\"STATUS\":\"A\",\"STATUSTIME\":\"2015-06-18 10:54:05\",\"TITLE\":\"丁军\"},{\"ID\":6597999.0,\"DEPTID\":1972999.0,\"USERNAME\":\"张伟国\",\"PASSWORD\":\"3C87E7540153D879\",\"LOGONID\":\"zhangwg\",\"DUTY\":\"副科长\",\"SEX\":\"男\",\"STATUS\":\"A\",\"STATUSTIME\":\"2014-12-22 15:20:46\",\"TITLE\":\"张伟国\"},{\"ID\":6599999.0,\"DEPTID\":1973999.0,\"USERNAME\":\"陈永斌\",\"PASSWORD\":\"3C87E7540153D879\",\"LOGONID\":\"chenyb\",\"DUTY\":\"副科长\",\"SEX\":\"男\",\"STATUS\":\"A\",\"STATUSTIME\":\"2014-12-22 15:24:25\",\"TITLE\":\"陈永斌\"},{\"ID\":6604999.0,\"DEPTID\":1974999.0,\"USERNAME\":\"毛喜峰\",\"PASSWORD\":\"3C87E7540153D879\",\"LOGONID\":\"maoxf\",\"DUTY\":\"副科长\",\"SEX\":\"男\",\"STATUS\":\"A\",\"STATUSTIME\":\"2014-12-22 15:34:12\",\"TITLE\":\"毛喜峰\"},{\"ID\":6614999.0,\"DEPTID\":1976999.0,\"USERNAME\":\"王伟\",\"PASSWORD\":\"3C87E7540153D879\",\"LOGONID\":\"wangw\",\"DUTY\":\"副科长\",\"SEX\":\"男\",\"STATUS\":\"A\",\"STATUSTIME\":\"2014-12-22 16:12:49\",\"TITLE\":\"王伟\"}]";
            DataTable dt6 = JSONHelper.JsonToObject<DataTable>(str6);


            //根据属性名获取属性值
            List<Users> list7 = new List<Users>() {
                new Users() {ID = 1111, USERNAME = "测试人员1", DUTY = "科长", SEX = "男" },
                new Users() {ID = 8888, USERNAME = "测试人员2", DUTY = "副科长", SEX = "女" }
            };
            var Data = new { rows = list7.Count, List = list7 };
            string json = JSONHelper.ObjectToJson(Data);
            string rows7 = JSONHelper.GetValueByKey(json, "rows");
            string name7 = JSONHelper.GetValueByKey(json, "List.0.USERNAME");

            //LinqToJson,无对象创建json
            List<object> list = new List<object>();
            list.Add("测试");
            list.Add(1);
            list.Add(new DateTime(2016, 2, 24));
            string json8 = JSONHelper.LinqToJson(list);
            string json_name = JSONHelper.LinqToJson(list, "test");
            return "";
        }


        private string EnumDemo(HttpContext context)
        {
            var a = "有说明版_直接输出枚举值:" + Week.Monday;//Monday
            var b = "有说明版_直接输出枚举信息:" + EnumHelper.GetDescription(Week.Monday);//星期一
            var c = "直接输出枚举值:" + NewWeek.星期一;//星期一
            var d = "直接输出枚举数值:" + EnumHelper.GetEnumValue<NewWeek>(NewWeek.星期一);//1
            return "";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        } 

        enum Week
        {
            [EnumDescription("星期一")]
            Monday = 1,
            [EnumDescription("星期二")]
            Tuesday
        }


        enum NewWeek
        { 
            星期一 = 1,
            星期二 = 2
        }
    }
}

