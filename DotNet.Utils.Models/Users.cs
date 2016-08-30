using System;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
   public  class Users
    {
        /// <summary>
        /// 标识列
        /// </summary>
        /// 
        [Column(Identity = true)]
        public int? ID { get; set; } 

        /// <summary>
        /// 部门标识列
        /// </summary>
        public int? DEPTID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PASSWORD { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LOGONID { get; set; }

        /// <summary>
        /// 用户代码    
        /// </summary>
        public string USERCODE { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string DUTY { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SEX { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCD { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string ADDR { get; set; }

        /// <summary>
        /// EMAIL
        /// </summary>
        public string EMAIL { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public int? SUPERID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS { get; set; }

        /// <summary>
        /// 状态时间
        /// </summary>
        public DateTime? STATUSTIME { get; set; }

        /// <summary>
        /// 头衔
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string ORDERS { get; set; }
    }
}
