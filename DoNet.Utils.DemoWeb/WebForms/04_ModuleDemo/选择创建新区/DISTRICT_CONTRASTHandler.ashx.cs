using DotNet.Utils;
using DotNet.Utils.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoNet.Utils.DemoWeb.WebForms._04_ModuleDemo.选择创建新区
{
    /// <summary>
    /// DISTRICT_CONTRASTHandler 的摘要说明
    /// </summary>
    public class DISTRICT_CONTRASTHandler : IHttpHandler
    {
        DISTRICT_CONTRASTManage dcm = new DISTRICT_CONTRASTManage();
        DISTRICTManage dm = new DISTRICTManage();

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            string resultStr = string.Empty;
            switch (action)
            {
                case "GetAll":
                    resultStr = GetAll(context);
                    break;
                case "GetAllDistrict":
                    resultStr = GetAllDistrict(context);
                    break;
                case "GetDistrictById":
                    resultStr = GetDistrictById(context);
                    break;
                case "Insert":
                    resultStr = Insert(context);
                    break;
                case "Update":
                    resultStr = Update(context);
                    break;
                case "Delete":
                    resultStr = Delete(context, false);
                    break;
                case "DeleteRoot":
                    resultStr = Delete(context, true);
                    break;

                default:
                    break;
            }
            context.Response.Write(resultStr);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetAll(HttpContext context)
        {
            return dcm.GetDISTRICTData(0, "DISTRICT_CONTRAST");
        }

        /// <summary>
        /// 获取编辑页面左侧树
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetAllDistrict(HttpContext context)
        {
            return dcm.GetDISTRICTData(1, "DISTRICT_OLD").Replace("NAME", "text").Replace("ID", "id");
        }

        /// <summary>
        /// 修改数据时,根据选中数据,显示右侧树
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetDistrictById(HttpContext context)
        {
            try
            {
                if (context.Request.Params["id"] != null)
                {
                    int id = Convert.ToInt32(context.Request.Params["id"]);
                    return dcm.GetDISTRICTDataForEdit(id).Replace("NAME", "text").Replace("ID", "id");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return "-1";
        }

        /// <summary>
        /// 增加新区数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Insert(HttpContext context)
        {
            try
            {
                ArrayList list = new ArrayList();
                string name = context.Request.Params["NAME"];
                //获取新区对象
                DISTRICT_CONTRAST fatherDC = new DISTRICT_CONTRAST()
                {
                    ID = dcm.GetSequence(),
                    NAME = name,
                    state = "closed",
                    SUPERID = 0
                };
                list.Add(dcm.GetInsertSql(fatherDC));
                string childrenName = context.Request.Params["childrenName"];
                string childrenId = context.Request.Params["childrenId"];
                string[] arrayName = childrenName.Substring(1).Split(',');
                string[] arrayId = childrenId.Substring(1).Split(',');
                for (int i = 0; i < arrayName.Length; i++)
                {
                    DISTRICT_CONTRAST dc = new DISTRICT_CONTRAST()
                    {
                        ID = dcm.GetSequence(),
                        SUPERID = fatherDC.ID,
                        NAME = arrayName[i],
                        SUPERNAME = fatherDC.NAME,
                        OLDID = Convert.ToInt32(arrayId[i])
                    };
                    list.Add(dcm.GetInsertSql(dc));
                }
                return dcm.ExecuteSqlTran(list).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return "-1";
            }
        }

        /// <summary>
        /// 修改新区数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Update(HttpContext context)
        {
            ArrayList list = new ArrayList();
            try
            {
                string name = context.Request.Params["NAME"];
                int id;
                if (context.Request.Params["ID"] != null)
                {
                    id = Convert.ToInt32(context.Request.Params["ID"]);
                    //获取更新对象以及其更新语句
                    DISTRICT_CONTRAST fatherDC = new DISTRICT_CONTRAST()
                    {
                        ID = id,
                        NAME = name
                    };
                    list.Add(dcm.GetUpdateSql(fatherDC));

                    //获取更新对象的原来子集对象,及其删除语句
                    DISTRICT_CONTRAST dcOldSon = new DISTRICT_CONTRAST() { SUPERID = id };
                    list.Add(dcm.GetDeleteSql(dcOldSon));

                    //获取更新对象的新子集对象,及其插入语句
                    string childrenName = context.Request.Params["childrenName"];
                    string childrenId = context.Request.Params["childrenId"];
                    string[] arrayName = childrenName.Substring(1).Split(',');
                    string[] arrayId = childrenId.Substring(1).Split(',');
                    for (int i = 0; i < arrayName.Length; i++)
                    {
                        DISTRICT_CONTRAST dc = new DISTRICT_CONTRAST()
                        {
                            ID = dcm.GetSequence(),
                            SUPERID = fatherDC.ID,
                            NAME = arrayName[i],
                            SUPERNAME = fatherDC.NAME,
                            OLDID = Convert.ToInt32(arrayId[i])
                        };
                        list.Add(dcm.GetInsertSql(dc));
                    }

                    return dcm.ExecuteSqlTran(list).ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return "-1";
        }

        /// <summary>
        /// 删除新区数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Delete(HttpContext context, bool isRoot)
        {
            int ID;
            try
            {
                if (context.Request.Params["ID"] != null)
                {
                    ID = Convert.ToInt32(context.Request.Params["ID"]);
                    DISTRICT_CONTRAST dc = new DISTRICT_CONTRAST() { ID = ID };
                    ArrayList list = new ArrayList();
                    if (isRoot)
                    {
                        DISTRICT_CONTRAST dcSon = new DISTRICT_CONTRAST() { SUPERID = ID };
                        list.Add(dcm.GetDeleteSql(dcSon));
                    }
                    list.Add(dcm.GetDeleteSql(dc));
                    return dcm.ExecuteSqlTran(list).ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return "-1";
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