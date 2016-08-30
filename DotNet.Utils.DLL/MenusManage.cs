using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils.DLL
{
    public class MenusManage : CRUDHelper<Menus>
    {

        public string GetMenusData()
        {
            string str = JSONHelper.ObjectToJson(GetTreeData(0));
            return str;
        }

        private List<Menus> GetTreeData(int id)
        {
            List<Menus> list = new List<Menus>();
            string str = "select * from MENUS where fatherId=" + id + " order by orders";
            try
            {
                DataTable dt = this.SelectBySQL(str);
                foreach (DataRow dr in dt.Rows)
                {
                    Menus trees = new Menus();
                    trees.ID = Convert.ToInt32(dr["Id"]);
                    trees.FATHERID = Convert.ToInt32(dr["FatherId"]);
                    trees.NAME = dr["Name"].ToString();
                    trees.URL = dr["Url"] == DBNull.Value ? "" : dr["Url"].ToString();
                    trees.ORDERS = Convert.ToInt32(dr["Orders"]);
                    trees.SELECTED = dr["SELECTED"] == DBNull.Value ? "" : dr["SELECTED"].ToString();
                    trees.ICON = dr["ICON"] == DBNull.Value ? "" : dr["ICON"].ToString();
                    trees.ChildrenList = GetTreeData(Convert.ToInt32(dr["Id"]));
                    list.Add(trees);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }

            return list;
        }
    }
}
