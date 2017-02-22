using DotNet.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoNet.Utils.DemoWeb.WebForms._06_ASPDemo
{
    public partial class LinqDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Users> UserList = new List<Users>
                {
                    new Users {USERNAME = "桑琪",  SEX="女",DUTY="科长",ID=1},
                    new Users {USERNAME = "苏敏",SEX="女",DUTY="主任",ID=2},
                    new Users {USERNAME = "丁晧", SEX="男",DUTY="副主任",ID=3},
                    new Users {USERNAME = "李金友", SEX="男",DUTY="科员",ID=4},
                    new Users {USERNAME = "李艳飞", SEX="女",DUTY="副局长",ID=5},
                    new Users {USERNAME = "马远德", SEX="男",DUTY="副科长",ID=6},
                    new Users {USERNAME = "崔佳样", SEX="男",DUTY="局长",ID=7},
                    new Users {USERNAME = "宋景尧", SEX="男",DUTY="科员",ID=8}
                };

                GridViewAll.DataSource = UserList;
                GridViewAll.DataBind();

                var selectNan = from p in UserList where p.SEX.Equals("男") orderby p.ID select p;
                GridViewNan.DataSource = selectNan;
                GridViewNan.DataBind();

                var selectNv = UserList.Where(p => p.SEX.Equals("女"));
                GridViewNv.DataSource = selectNv;
                GridViewNv.DataBind();

                var selectLi = UserList.Where(p => p.USERNAME.StartsWith("李")).OrderByDescending(p => p.ID);
                GridViewLi.DataSource = selectLi;
                GridViewLi.DataBind();

                var selectPage = UserList.Skip(4).Take(2);
                GridViewPage.DataSource = selectPage;
                GridViewPage.DataBind();
            }
        }
    }
}