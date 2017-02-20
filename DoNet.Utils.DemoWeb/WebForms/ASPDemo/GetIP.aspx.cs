using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoNet.Utils.DemoWeb.WebForms.ASPDemo
{
    public partial class GetIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string AddressIP = string.Empty;
                foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        AddressIP = _IPAddress.ToString();
                        fwqIP.Text ="服务器IP地址为:"+ AddressIP;
                        khdIP.Text += "客户端IP地址为:" + System.Web.HttpContext.Current.Request.UserHostAddress ; 
                        break;
                    }
                } 
            }
        }
    }
}