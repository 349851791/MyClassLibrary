using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DotNet.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class TLWCodeHelper : CRUDHelper<CODES>
    {
        /// <summary>
        /// 获取唯一值
        /// </summary>
        /// <param name="myCode">实体类的条件</param>
        /// <param name="columnName">获取唯一值的列名,默认codevalue</param>
        /// <returns></returns>
        public object GetValue(CODES myCode, string columnName = "codevalue")
        {
            string strCondition = InternalBase<CODES>.GetParamForSelectAndDelete(myCode);
            string sql = "select " + columnName + " from CODES " + strCondition;
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
        /// <param name="codes">实体类的条件</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public DataTable GetData(CODES codes, string groupName = "CODEORDER", string columnName = "*")
        {
            return this.Select(codes, groupName, columnName);
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public DataTable GetData(string typeName, string groupName = "CODEORDER", string columnName = "*")
        {
            CODES codes = new CODES() { TYPENAME = typeName };
            return this.Select(codes, groupName, columnName);
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="codes">实体类的条件</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public List<CODES> GetDataForList(CODES codes, string groupName = "CODEORDER", string columnName = "*")
        {
            return DataTableHelper<CODES>.ConvertToModel(GetData(codes, groupName, columnName)).ToList(); 
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public List<CODES> GetDataForList(string typeName, string groupName = "CODEORDER", string columnName = "*")
        {
            CODES codes = new CODES() { TYPENAME = typeName };
            return DataTableHelper<CODES>.ConvertToModel(GetData(codes, groupName, columnName)).ToList();
        }


        /// <summary>
        ///  根据条件获取数据,返回针对easyuiComBox使用的字符串
        /// </summary>
        /// <param name="codes">实体类的条件</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param>
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public string GetDataForComBox(CODES codes, string groupName = "CODEORDER", string columnName = "*")
        {
            return JSONHelper.ObjectToJson(GetData(codes, groupName, columnName)).Replace("CODENAME", "id").Replace("CODEVALUE", "text");
        }

        /// <summary>
        ///  根据条件获取数据,返回针对easyuiComBox使用的字符串
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="groupName">排序字段:写入order by之后的内容,默认:GROUPID</param>
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public string GetDataForComBox(string typeName, string groupName = "CODEORDER", string columnName = "*")
        { 
            return JSONHelper.ObjectToJson(GetData(typeName, groupName, columnName)).Replace("CODENAME", "id").Replace("CODEVALUE", "text");
        }
    }

    /// <summary>
    /// CODES
    /// </summary>
    public class CODES
    {
        /// <summary>
        /// 
        /// </summary> 
        public string TYPENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CODENAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CODEVALUE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CODEORDER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GRADE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SUPERCODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FLAG { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int? ID { get; set; }

    }
}
