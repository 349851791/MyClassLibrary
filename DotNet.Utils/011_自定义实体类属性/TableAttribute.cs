using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{
    
    //AttributeTargets.Class表示只可用于类，所以使用时把该属性加载类的上面
    //AllowMultiple 表示能否为一个元素指定多个属性示例，设置false即只可配置一次。
    //Inherited 表示Table属性可否被继承，设置false即不可被继承。

    /// <summary>
    /// 实体类的自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 实体类对应的数据库表名
        /// </summary>
        public string Name { get; set; }
       
    }
}
#region 过期内容
//private string _Name = string.Empty;

//public string Name
//{
//    get { return _Name; }
//    set { _Name = value; }
//}

//public TableAttribute()
//{
//    NoAutomaticKey = false;
//} 

///// <summary>
///// 不具备自增长键的表
///// </summary>
//public bool NoAutomaticKey { get; set; }
#endregion
