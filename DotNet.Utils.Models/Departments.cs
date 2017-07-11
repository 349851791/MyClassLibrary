
using System;

namespace DotNet.Utils.Models
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class Departments
    {
        /// <summary>
        /// 标识列
        /// </summary>
        [Column(Identity = true)]
        public int? ID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? ORDERS { get; set; }

        /// <summary>
        /// 领导ID
        /// </summary>
        public int? LEADERID { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public int? SUPERDEPTID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DEPTNAME { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        public string ABBR { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        public string DEPTKIND { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 传真dianhua
        /// </summary>
        public string FAX { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS { get; set; }

        /// <summary>
        /// 状态时间
        /// </summary>
        public DateTime? STATUSTIME { get; set; }
    }

    public class DepartmentsManage : CRUDHelper<Departments>
    {
    }
}
