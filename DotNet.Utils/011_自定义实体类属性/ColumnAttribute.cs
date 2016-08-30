/// <summary>
/// 使用前提： 
/// 类 说 明： 根据枚举值获取自定义的值
/// 编 码 人： 张贺
/// 创建日期： 
/// 更新日期： 20160329
/// 更新内容:  增加列属性,是否为标识列
/// </summary>

using System; 

namespace DotNet.Utils
{
    /// <summary>
    /// 实体类属性的自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 实体类的属性,对应的数据库列名
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 实体类的属性,对应的类的类型,针对于有时时间类型字段,从有修改到无时使用,修改到无了,无法使用反射判断了!
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否标识列
        /// </summary>
        public bool Identity { get; set; }
    }
}

#region 过期内容
//private string _Name = string.Empty;//列名        
//private bool _IsUnique = false;//是否唯一        
//private bool _IsNull = true;//是否允许为空
//private bool _IsInsert = true;//是否插入到表中
//private bool _IsUpdate = true;//是否修改到表中
//private bool _Ignore = false;//是否修改到表中

//public string Name
//{
//    get { return _Name; }
//    set { _Name = value; }
//}

//public bool IsUnique
//{
//    get { return _IsUnique; }
//    set { _IsUnique = value; }
//}

//public bool IsNull
//{
//    get { return _IsNull; }
//    set { _IsNull = value; }
//}

//public bool IsInsert
//{
//    get { return _IsInsert; }
//    set { _IsInsert = value; }
//}

//public bool IsUpdate
//{
//    get { return _IsUpdate; }
//    set { _IsUpdate = value; }
//}

//public bool Ignore
//{
//    get { return _Ignore; }
//    set { _Ignore = value; }
//}
#endregion