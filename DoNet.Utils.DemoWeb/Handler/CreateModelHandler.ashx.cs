using DotNet.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace DoNet.Utils.DemoWeb.Handler
{
    /// <summary>
    /// CreateModelHandler 的摘要说明
    /// </summary>
    public class CreateModelHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        CRUDHelper<object> crudHelper;
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "createConnection":
                    resultStr = createConnection(context);
                    break;
                case "GetTableByDataBase":
                    resultStr = GetTableByDataBase(context);
                    break;
                case "GetColumnByTable":
                    resultStr = GetColumnByTable(context);
                    break;
                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        //创建链接字符串
        private string createConnection(HttpContext context)
        {
            string resultStr = "1";
            try
            {
                string username = context.Request.Params["username"];
                string userpsd = context.Request.Params["userpsd"];
                string orclname = context.Request.Params["orclname"];
                string strConnection = $"Data Source={orclname};Persist Security Info=True;User={username};Password={userpsd};Unicode=True";
                string strType = "Oracle";

                HttpContext.Current.Session["strConnection"] = strConnection;
                HttpContext.Current.Session["strType"] = strType;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                resultStr = ex.Message;
            }
            return resultStr;

        }

        /// <summary>
        ///根据数据库获取所有数据表 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetTableByDataBase(HttpContext context)
        {
            crudHelper = new CRUDHelper<object>(HttpContext.Current.Session["strConnection"]?.ToString(), HttpContext.Current.Session["strType"]?.ToString());
            // crudHelper = new CRUDHelper<object>(globalVar.strConnection, globalVar.strType); 
            int page = Convert.ToInt32(context.Request.Params["page"]);
            int rows = Convert.ToInt32(context.Request.Params["rows"]);  
            return crudHelper.SelectPageToJSONBySQL("user_tab_comments", page, rows, "TABLE_NAME");
        }

        /// <summary>
        /// 根据数据表获取列,并且输出实体类文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetColumnByTable(HttpContext context)
        {
            try
            {
                crudHelper = new CRUDHelper<object>(HttpContext.Current.Session["strConnection"]?.ToString(), HttpContext.Current.Session["strType"]?.ToString());
                string table = context.Request.Params["table"];
                Array arr = table.Split(',');
                string currentPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "WebForms\\CreateModel\\ModelFile";
                //清除文件夹中所有文件
                if (!FolderHelper.IsEmptyDirectory(currentPath))
                {
                    FolderHelper.DeleteFolder(currentPath, false);
                }

                foreach (string item in arr)
                {
                    string sql = $"select t.column_name names,t.comments,u.DATA_TYPE types from user_col_comments t join user_tab_cols u on t.column_name=u.COLUMN_NAME where t.table_name='{item}' and u.TABLE_NAME='{item}' order by u.INTERNAL_COLUMN_ID";
                    DataTable data = crudHelper.SelectBySQL(sql);
                    string className = item.ToUpper();
                    StringBuilder classStr = new StringBuilder();
                    bool isIdentity = true;
                    classStr.Append(" using DotNet.Utils;\n");
                    classStr.Append(" using System;\n");
                    classStr.Append(" namespace Models\n{\n");
                    classStr.Append("  /// <summary>\n/// 用户表\n/// </summary>\n");
                    classStr.Append("  public  class " + className + "\n{\n");

                    foreach (DataRow dr in data?.Rows)
                    {
                        TempTable tt = new TempTable()
                        {
                            names = dr["NAMES"].ToString(),
                            types = dr["TYPES"].ToString(),
                            comments = dr["COMMENTS"].ToString()
                        };
                        classStr.Append("  /// <summary>\n/// " + tt.comments + "\n/// </summary>\n");
                        if (isIdentity)
                        {
                            classStr.Append("[Column(Identity =true)]\n");
                            isIdentity = false;
                        }
                        classStr.Append("  public  " + GetModelType(tt.types) + tt.names + " { get; set; }\n\n");
                    }
                    classStr.Append(" }\n");
                    classStr.Append("public class "+ className + "Manage : CRUDHelper<"+ className + ">\n{ \n}\n"); 
                    classStr.Append(" }\n"); 
                    string path = currentPath + "\\" + className + ".cs";
                    FileHelper.Write(path, classStr.ToString());
                }
                string err = "";
                bool bZip = ZipHelper.ZipFile(currentPath, currentPath + "\\Model.Zip", out err);
                if (bZip)
                {
                    return "1";
                }
                else
                {
                    return err;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 根据数据库表列类型返回实体类字段类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        string GetModelType(string dataType)
        {
            string str = string.Empty;
            if (dataType.Contains("VARCHAR2"))
            {
                str = " string ";
            }
            else if (dataType.Equals("NUMBER"))
            {
                str = " int? ";
            }
            else if (dataType.Equals("DATE"))
            {
                str = " DateTime? ";
            }
            else if (dataType.Contains("NUMBER("))
            {
                str = " double? ";
            }
            else
            {
                str = " string ";
            }
             
            return str;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    internal class TempTable
    {
        internal string names { get; set; }
        internal string types { get; set; }
        internal string comments { get; set; }
    }
}