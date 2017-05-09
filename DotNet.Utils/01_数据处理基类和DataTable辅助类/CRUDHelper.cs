///更新记录:
///更新日期:2015-11
///更新内容: 1. 增加异常内容
///更新日期:2015-12-08
///更新内容: 1. dic模式 修改GetConditionByDIC方法, 解决or和like问题
///          2. 增加查询时的列名参数,默认为:*
///          3. 增加实体类和实体类属性的自定义属性,可以在实体类中和数据库表进行映射.
///             修改CRUDHelper类的GetTableName方法,直接获取表名,以及修改调用其的方法.
///更新日期:2015-12-30
///更新内容: 1. 增加、删除、修改加入返回sql语句的方法      
///更新日期:2016-1-5
///更新内容: 1. 修改返回json的分页查询方法,调用返回datatable的方法,增加代码重用性
///更新日期:2016-03-25
///更新内容: 1. GetParamForSelectAndDelete 方法加入空字符判断
///          2. 修改GetSequence方法
///更新日期:2016-03-29
///更新内容: 1. 删除根据名称和ID获取对象方法,增加根据标识列返回对应数据方法(SelectByIdentity),
///                                          增加根据列名和列值返回对应数据方法(SelectByColumn)
///          2. 删除返回实体类字段的自定义类型方法,增加返回字段自定义属性方法(GetColumnAttribute)
///          3. 修改返回最大值和最小值方法
///          4. 加入构造函数,可以在代码中设置连接字符串
///          5. 修改字典的删除方法,不再需要传入表名
///更新日期:2016-05-30
///更新内容: 1. 增加方法 :SelectPage,GetSelectSql,SelectPageToJSONBySQL
///更新日期:2016-06-2
///更新内容: 更改较大.将方法内代码提出.分别放入4个其他的类中.
///          1. 字典类代码名称去掉ByDic,变为方法的重载.
///          2. 字典类方法不再必须传入对象类型参数,增加表名参数.
///          3. 部分代码修改为c#6模式.
///          4. 修改部分方法说明,升级版本号为2.0

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DotNet.Utils
{

    /// <summary>
    /// 增删改查帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CRUDHelper<T> where T : class
    {
        DataBaseLayer dbl = new DataBaseLayer();
        AssistBase<T> ab = new AssistBase<T>();
        ObjectBase<T> objectBase = new ObjectBase<T>();
        DicBase<T> dicBase = new DicBase<T>();

        #region 构造函数
        public CRUDHelper()
        {

        }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="strConnect"></param>
        /// <param name="dataType"></param>
        public CRUDHelper(string strConnect, string dataType)
        {
            dbl = new DataBaseLayer(strConnect, dataType);
        }
        #endregion

        #region 执行DataBaseLayer中的部分方法
        /// <summary>
        /// 根据sql语句进行查询,返回数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable SelectBySQL(string sql)
        {
            return dbl.ExecuteQuery(sql);
        }

        /// <summary>
        /// 根据sql语句进行查询,返回一条计算过的数据.如:count max min 等
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public int SelectScalar(string sql)
        {
            return ab.SelectScalar(sql);
        }

        /// <summary>
        /// 根据sql语句进行查询,返回唯一的字符串
        /// </summary>
        /// <param name="sql">查询语句</param> 
        /// <returns></returns>
        public  string GetOnlyColumnValue(string sql)
        {
            DataBaseLayer dbl = new DataBaseLayer();
            DataTable dt = dbl.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 执行多条sql语句,实现事务
        /// </summary>
        /// <param name="aList">sql语句集合</param>
        /// <returns></returns>
        public int ExecuteSqlTran(System.Collections.ArrayList aList)
        {
            return dbl.ExecuteSqlTran(aList);
        }
        #endregion

        #region 辅助查询方法

        /// <summary>
        /// 根据标识列的值返回整条数据
        /// </summary> 
        /// <param name="IdentityValue">标识列的值</param>
        /// <returns></returns>
        public DataTable SelectByIdentity(string IdentityValue)
        {
            return ab.SelectByIdentity(IdentityValue);
        }

        /// <summary>
        /// 根据列以及列的值,返回对应的一条数据
        /// </summary>
        /// <param name="ColumnName">列名</param>
        /// <param name="ColumnValue">列值</param>
        /// <returns></returns>
        public DataTable SelectByColumn(string ColumnName, string ColumnValue)
        {
            return ab.SelectByColumn(ColumnName, ColumnValue);
        }

        /// <summary>
        /// 获取序列
        /// </summary>
        /// <param name="seqName">序列名:如果 序列名=SEQ_实体类名,则可以不传入参数</param>
        /// <returns></returns>
        public int GetSequence(string seqName = "")
        {
            return ab.GetSequence(seqName);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="columnName">最大值的列名</param>
        /// <param name="t">实体类,传入的值都将当做查询条件参数</param>
        /// <returns></returns>
        public int GetMax(string columnName, T t = null)
        {
            return ab.GetMax(columnName, t);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="columnName">最大值的列名</param> 
        /// <param name="t">实体类,传入的值都将当做查询条件参数</param>
        /// <returns></returns>
        public int GetMin(string columnName, T t = null)
        {
            return ab.GetMin(columnName, t);
        }
        #endregion

        #region 返回sql语句

        /// <summary>
        /// 返回查询语句
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="strOrder">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>
        /// <param name="strCondition">条件,需要填写where</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public string GetSelectSql(string strTable, int pageCurrent, int pageSize, string strOrder, string strCondition, string columnName)
        {
            return dicBase.GetSelectSql(strTable, pageCurrent, pageSize, strOrder, strCondition, columnName);
        }

        #region 对象返回sql语句
        /// <summary>
        /// 获取insert语句
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <returns></returns>
        public string GetInsertSql(T t)
        {
            return objectBase.GetInsertSql(t);
        }

        /// <summary>
        /// 获取update语句
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <param name="conditionName">条件字段,默认为ID,如果有多个条件,可以用","隔开</param>
        /// <returns></returns>
        public string GetUpdateSql(T t, string conditionName = "ID")
        {
            return objectBase.GetUpdateSql(t, conditionName);
        }

        /// <summary>
        /// 获取delete语句
        /// </summary>
        /// <param name="t">实体类型</param> 
        /// <returns></returns>
        public string GetDeleteSql(T t)
        {
            return objectBase.GetDeleteSql(t);
        }
        #endregion

        #region 字典返回sql语句

        /// <summary>
        /// 根据字典,返回分页需要是sql语句
        /// </summary>
        /// <param name="dataSql">返回查询datatable的sql</param>
        /// <param name="countSql">返回查询数量的sql</param>
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public void GetSelectPageSql(out string dataSql, out string countSql, Dictionary<string, object> dic, String order, int pageCurrent, int pageSize = 10, string tableName = "", string columnName = "*")
        {
             dicBase.GetSelectPageSql(out dataSql,out countSql, dic, order, pageCurrent, pageSize, tableName, columnName);
        }

        /// <summary>
        /// 获取插入语句,根据字典.
        /// </summary>
        /// <param name="columnDic">列数据字典,如果列为日期格式,需要增加后缀__date</param> 
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string GetInsertSql(Dictionary<string, object> columnDic, string tableName = "")
        {
            return dicBase.GetInsertSql(columnDic, tableName);
        }


        /// <summary>
        /// 获取增加语句,根据字典.条件为字典格式,有些条件为时间段,无法用实体类表示.关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)
        /// </summary> 
        /// <param name="columnDic">条件字典</param>
        /// <param name="conditionDic">条件字典</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string GetUpdateSql(Dictionary<string, object> columnDic, Dictionary<string, object> conditionDic, string tableName)
        {
            return dicBase.GetUpdateSql(columnDic, conditionDic, tableName);
        }

        /// <summary>
        /// 获取delete语句.根据条件字典,用于有时间段的条件
        /// </summary> 
        /// <param name="dic">条件字典</param>
        /// <param name="tableName">表名 </param>
        /// <returns></returns>
        public string GetDeleteSql(Dictionary<string, object> dic, string tableName = "")
        {
            return dicBase.GetDeleteSql(dic, tableName);
        }
        #endregion

        #endregion

        #region 表格查询方法

        #region 根据sql语句,返回数据

        /// <summary>
        /// 查询显示分页后的列表数据
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="strOrder">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>
        /// <param name="strCondition">条件,需要填写where</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable SelectPageWithoutObject(string strTable, int pageCurrent, string strOrder = "", String strCondition = "", int pageSize = 10, string columnName = "*")
        {
            string sql = GetSelectSql(strTable, pageCurrent, pageSize, strOrder, strCondition, columnName);
            return dbl.ExecuteQuery(sql);
        }


        /// <summary>
        /// 查询显示分页数据.返回json字符串(包含列表数据和总数据量数据,配合easyui使用),根据实体对象生成条件.
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>
        /// <param name="strCondition">条件,需要填写where</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public string SelectPageToJSONBySQL(string strTable, int pageCurrent, int pageSize = 10, string order = "", String strCondition = "", string columnName = "*")
        {
            string sql = "";
            string countSql = "";
            try
            {
                string strOrder = InternalBase<T>.AddOrder(order);//获取排序字段. 
                sql = this.GetSelectSql(strTable, pageCurrent, pageSize, strOrder, strCondition, columnName);
                int number = (pageCurrent - 1) * pageSize;
                DataTable dt = this.SelectBySQL(sql);
                countSql = "select count (*) from " + strTable + strCondition;
                var pageCount = dbl.GetSingle(countSql);
                return JSONHelper.ObjectToJson(new { total = pageCount, rows = dt });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex, sql + "\r\n" + countSql);
                return "";
            }
        }
        #endregion

        #region 根据对象,返回数据 

        /// <summary>
        /// 查询返回所有数据,根据实体对象生成条件.
        /// </summary> 
        /// <param name="t">实体类,将查询条件赋给实体类的属性</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>   
        /// <param name="columnName">列名,默认为*,查询所有</param>
        /// <returns></returns>
        public DataTable Select(T t, string order = "", string columnName = "*")
        {
            return objectBase.Select(t, order, columnName);
        }


        /// <summary>
        /// 查询显示分页后的列表数据,根据实体对象生成条件.
        /// </summary>
        /// <param name="t">实体类,将查询条件赋给实体类的属性</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable SelectPage(T t, String order, int pageCurrent, int pageSize = 10, string columnName = "*")
        {
            return objectBase.SelectPage(t, order, pageCurrent, pageSize, columnName);
        }

        /// <summary>
        /// 查询显示分页数据.返回json字符串(包含列表数据和总数据量数据,配合easyui使用),根据实体对象生成条件.
        /// </summary>
        /// <param name="t">实体类,将查询条件赋给实体类的属性</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public string SelectPageToJSON(T t, String order, int pageCurrent, int pageSize = 10, string columnName = "*")
        {
            return objectBase.SelectPageToJSON(t, order, pageCurrent, pageSize, columnName);
        }
        #endregion

        #region  根据字典,返回数据
        /// <summary>
        /// 查询返回所有数据,根据字典生成条件,用于时间条件为时间段时.
        /// </summary> 
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>
        /// <param name="tableName">表名</param> 
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable Select(Dictionary<string, object> dic, string order = "", string tableName = "", string columnName = "*")
        {
            return dicBase.Select(dic, order, tableName, columnName);
        }

        /// <summary>
        /// 查询显示分页后的列表数据.根据字典生成条件,用于时间条件为时间段时.
        /// </summary>
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public DataTable SelectPage(Dictionary<string, object> dic, String order, int pageCurrent, int pageSize = 10, string tableName = "", string columnName = "*")
        {
            return dicBase.SelectPage(dic, order, pageCurrent, pageSize, tableName, columnName);
        }


        /// <summary>
        /// 查询显示分页数据.返回json字符串(包含列表数据和总数据量数据,配合easyui使用).根据字典生成条件,用于时间条件为时间段时.
        /// </summary>
        /// <param name="dic">保存条件的字典,key为字段名称,关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)</param>
        /// <param name="order">排序字段:如果不需要排序可不传入数据,也可以传入null或者"";如果需要排序则写入order by之后的内容</param>          
        /// <param name="pageCurrent">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名,默认为*,查询所有</param> 
        /// <returns></returns>
        public string SelectPageToJSON(Dictionary<string, object> dic, String order, int pageCurrent, int pageSize = 10, string tableName = "", string columnName = "*")
        {
            return dicBase.SelectPageToJSON(dic, order, pageCurrent, pageSize, tableName, columnName);
        }

        #endregion

        #endregion

        #region 增删改方法

        #region 对象增删改
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <returns></returns>
        public int Insert(T t)
        {
            return objectBase.Insert(t);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <param name="conditionName">条件字段,默认为ID,如果有多个条件,可以用","隔开</param>
        /// <returns></returns>
        public int Update(T t, String conditionName = "ID")
        {
            return objectBase.Update(t, conditionName);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t">实体类型</param> 
        /// <returns></returns>
        public int Delete(T t)
        {
            return objectBase.Delete(t);
        }
        #endregion

        #region 字典增删改
        /// <summary>
        /// 获取插入语句,根据字典.
        /// </summary>
        /// <param name="columnDic">列数据字典,如果列为日期格式,需要增加后缀__date</param> 
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Insert(Dictionary<string, object> columnDic, string tableName = "")
        {
            return dicBase.Insert(columnDic, tableName);
        }


        /// <summary>
        /// 更新,条件为字典格式,有些条件为时间段,无法用实体类表示.关键字:数据库字段名__q(时间起),数据库字段名__z(时间止),数据库字段名__or(条件为or),数据库字段名__like(查询为like)
        /// </summary> 
        /// <param name="columnDic">列字典</param>
        /// <param name="conditionDic">条件字典</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Update(Dictionary<string, object> columnDic, Dictionary<string, object> conditionDic, string tableName = "")
        {
            return dicBase.Update(columnDic, conditionDic, tableName);
        }


        /// <summary>
        /// 删除,根据条件字典,用于有时间段的条件
        /// </summary> 
        /// <param name="dic">条件字典</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public int Delete(Dictionary<string, object> dic, string tableName = "")
        {
            return dicBase.Delete(dic, tableName);
        }

        #endregion

        #endregion

    }
}