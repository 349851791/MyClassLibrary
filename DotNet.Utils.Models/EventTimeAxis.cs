using System;
using System.Xml.Linq;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 事件时间轴表
    /// </summary> 
    public class EventTimeAxis
    {
        /// <summary>
        /// 标识列
        /// </summary>
        /// 
        [Column(Identity = true)]
        public int? ID { get; set; }

        /// <summary>
        /// 事件日期
        /// </summary>
        public DateTime? DATES { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string CONTENTS { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string HREFS { get; set; }
         

        /// <summary>
        /// 排序
        /// </summary>
        public int? ORDERS { get; set; }
    }
    public class EventTimeAxisManage : CRUDHelper<EventTimeAxis>
    {
    }
}
