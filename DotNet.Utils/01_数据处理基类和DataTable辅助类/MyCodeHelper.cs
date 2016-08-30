/// <summary>
/// 使用前提： 针对公司code表
/// 类 说 明： 
/// 编 码 人： 张贺
/// 创建日期： 2015-12-13
/// 更新日期： 
/// 更新内容:
/// </summary> 

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{
    /// <summary>
    /// MyCode帮助类
    /// </summary>
    public class MyCodeHelper: CRUDHelper<MYCODE>
    { 
        /// <summary>
        /// 获取唯一值
        /// </summary>
        /// <param name="myCode">实体类的条件</param>
        /// <param name="columnName">获取唯一值的列名,默认codevalue</param>
        /// <returns></returns>
        public object GetValue(MYCODE myCode, string columnName = "codevalue")
        {
            string strCondition = InternalBase<MYCODE>.GetParamForSelectAndDelete(myCode);
            string sql = "select " + columnName + " from MYCODE " + strCondition;
            DataTable dt = this.SelectBySQL(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][columnName];
            }
            return "";
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="myCode">实体类的条件</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public DataTable GetAllData(MYCODE myCode, string groupName = "GROUPID", string columnName = "*")
        {
            return this.Select(myCode, groupName,columnName );
        }

        /// <summary>
        ///  根据条件获取数据,返回针对easyuiComBox使用的字符串
        /// </summary>
        /// <param name="myCode">实体类的条件</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param>
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public string GetDataForComBox(MYCODE myCode, string groupName = "GROUPID", string columnName = "*")
        {
            return JSONHelper.ObjectToJson(GetAllData(myCode, groupName, columnName)).Replace("CODENAME", "id").Replace("CODEVALUE", "text");
        }

    }

    /// <summary>
    /// MYCODE实体类
    /// </summary>
    [Table(Name = "MYCODE")]
   
    public class MYCODE
    {
        /// <summary>
        /// 标识列
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int? FatherId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string CodeType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string CodeValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public string IsShow { get; set; }

        /// <summary>
        /// 其他值
        /// </summary>
        public string OtherValue { get; set; }

        /// <summary>
        /// 节点是否展开.closed:关闭;open:打开
        /// </summary>
        public string State { get; set; }

        ///// <summary>
        ///// 下拉框知否选中:true选中
        ///// </summary>
        //public string Selected { get; set; }
    }
}
