/// <summary>
/// 使用前提：  
/// 类 说 明： 关于url的操作
/// 编 码 人： 张贺
/// 创建日期： 2016-02-24
/// 更新日期： 
/// 更新内容:
/// </summary>
using System.Web;

namespace DotNet.Utils
{
    /// <summary>
    /// 设当前页完整地址是：http://www.web.com/aaa/bbb.aspx?id=5&name=admin
    /// "http://"是协议名
    /// "www.jb51.net"是域名
    /// "aaa"是站点名
    /// "bbb.aspx"是页面名（文件名）
    /// "id=5&name=kelli"是参数
    /// </summary>
    public class UrlHelper
    {

        /// <summary>
        /// 获取 完整url （协议名+域名+站点名+文件名+参数)
        /// 例如:http://www.web.com/aaa/bbb.aspx?id=5&name=admin
        /// </summary>
        /// <returns></returns>
        public static string GetAllUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        /// <summary>
        /// 获取 域名
        /// 例如:www.web.com
        /// </summary>
        /// <returns></returns>
        public static string GetHostUrl()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获取 站点名+页面名+参数
        /// 例如:/aaa/bbb.aspx?id=5&name=admin
        /// </summary>
        /// <returns></returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
            //return  HttpContext.Current.Request.Url.PathAndQuery;
        }
        /// <summary>
        /// 获取 站点名+页面名
        /// 例如:aaa/bbb.aspx
        /// </summary>
        /// <returns></returns>
        public static string GetPathUrl()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
            //return  HttpContext.Current.Request.Path;
        }

        /// <summary>
        /// 获取 参数
        /// 例如:?id=5&name=admin
        /// </summary>
        /// <returns></returns>
        public static string GetParam()
        {
            return HttpContext.Current.Request.Url.Query;
        }

        /// <summary>
        ///Request.RawUrl：获取客户端请求的URL信息（不包括主机和端口）------>/ Default2.aspx
        ///Request.ApplicationPath：获取服务器上ASP.NET应用程序的虚拟路径。------>/
        ///Request.CurrentExecutionFilePath：获取当前请求的虚拟路径。------>/ Default2.aspx
        ///Request.Path：获取当前请求的虚拟路径。------>/ Default2.aspx
        ///Request.PathInfo：取具有URL扩展名的资源的附加路径信息------ >
        ///Request.PhysicalPath：获取与请求的URL相对应的物理文件系统路径。------> E:\temp\Default2.aspx
        ///Request.Url.LocalPath：------>/ Default2.aspx
        ///Request.Url.AbsoluteUri：------> http://localhost:8080/Default2.aspx
        ///Request.Url.AbsolutePath：---------------------------->/ Default2.aspx
        /// </summary>
        /// <returns></returns>
        public static void ShowOther()
        {
        }
    }
}
