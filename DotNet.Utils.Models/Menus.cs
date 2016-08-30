
using System.Collections.Generic; 

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
}
