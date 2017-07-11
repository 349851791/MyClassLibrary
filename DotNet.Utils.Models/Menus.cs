
using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table(Name = "Menus")]
    public class Menus
    {
        /// <summary>
        /// 标识列
        /// </summary>
        public int? ID { get; set; }
        
        /// <summary>
        /// 父级标识列
        /// </summary>
        public int? FATHERID { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int ORDERS { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int? ISSHOW { get; set; }
        
        /// <summary>
        /// 功能路径
        /// </summary>
        public string URL { get; set; } 


        /// <summary>
        /// 图标样式
        /// </summary>
        public string ICON { get; set; }


        /// <summary>
        /// 默认是否展开
        /// </summary>
        public string SELECTED { get; set; }

        /// <summary>
        /// 子数据集合
        /// </summary>
        public List<Menus> ChildrenList { get; set; }
    }


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
