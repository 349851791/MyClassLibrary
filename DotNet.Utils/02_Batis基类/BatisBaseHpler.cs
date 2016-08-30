
/// 使用前提： 继承此类.例:    public class ProductManager : BatisBaseHpler<Product>
/// 类 说 明： batis的基类,以及辅助类
/// 编 码 人： 张贺
/// 创建日期： 2015-10-14
/// 更新日期： 
/// 更新内容:

using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration.Statements;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DotNet.Utils
{
   public  class BatisBaseHpler<T> where T : class
    {

        #region 基本增删改查操作
        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="statementName">声明名称</param>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public  int Insert(string statementName, T t)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return (int)iSqlMapper.Insert(statementName, t);
            }
            return 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="statementName">声明名称</param>
        /// <param name="primaryKeyId">主键ID</param>
        /// <returns></returns>
        public  int Delete(string statementName, int primaryKeyId)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Delete(statementName, primaryKeyId);
            }
            return 0;
        }

        /// <summary>
        ///修改 
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="statementName">声明名称</param>
        /// <param name="t"></param>
        /// <returns>实体对象</returns>
        public  int Update(string statementName, T t)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.Update(statementName, t);
            }
            return 0;
        }

        /// <summary>
        /// 返回实体对象个体
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="statementName">声明名称</param>
        /// <param name="primaryKeyId">主键ID</param>
        /// <returns></returns>
        public  T Get(string statementName, int primaryKeyId)  
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForObject<T>(statementName, primaryKeyId);
            }
            return null;
        }

        /// <summary>
        /// 返回实体对象集合
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="statementName">声明名称</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        public  IList<T> QueryForList(string statementName, object parameterObject = null)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForList<T>(statementName, parameterObject);
            }
            return null;
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取动态生成的sql语句
        /// </summary>
        /// <param name="stateMentName">声明名称</param>
        /// <param name="paramObject">参数</param>
        /// <returns></returns>
        public  string GetSql(string stateMentName, object paramObject)
        {
            ISqlMapper sqlMapper = Mapper.Instance();
            string resultsql = string.Empty;
            try
            {
                IMappedStatement statement = sqlMapper.GetMappedStatement(stateMentName);
                if (!sqlMapper.IsSessionStarted)
                {
                    sqlMapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMapper.LocalSession);
                resultsql = scope.PreparedStatement.PreparedSql;
            }
            catch (Exception ex)
            {
                resultsql = "获取SQL语句出现异常:" + ex.Message;
            }
            return resultsql;
        }


        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlMapper">ISqlMapper</param>
        /// <param name="statementName">statement的id</param>
        /// <param name="paramObject">sql语句的参数</param>
        /// <returns>DataTable</returns>
        public  DataTable GetDataTable(string statementName, object paramObject = null, string tableName = null, int tableIndex = 0)
        {
            ISqlMapper sqlMapper = Mapper.Instance();
            DataSet ds = new DataSet();
            IMappedStatement statement = sqlMapper.GetMappedStatement(statementName);
            if (!sqlMapper.IsSessionStarted)
            {
                sqlMapper.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMapper.LocalSession);

            statement.PreparedCommand.Create(scope, sqlMapper.LocalSession, statement.Statement, paramObject);

            IDbCommand cmd = GetDbCommand(sqlMapper, statementName, paramObject);
            sqlMapper.LocalSession.CreateDataAdapter(cmd).Fill(ds);
            if (tableName != null)
            {
                return ds.Tables[tableName];
            }
            else
            {
                return ds.Tables[tableIndex];
            }
            //获取ds里第一个DataTable   ds.Tables[0];
        }


        /// <summary>
        /// 获取DbCommand
        /// </summary>
        /// <param name="sqlMapper">ISqlMapper</param>
        /// <param name="statementName">statement的id</param>
        /// <param name="paramObject">sql语句的参数</param>
        /// <returns>DbCommand</returns>
        private  IDbCommand GetDbCommand(ISqlMapper sqlMapper, string statementName, object paramObject)
        {
            IStatement statement = sqlMapper.GetMappedStatement(statementName).Statement;
            IMappedStatement mapStatement = sqlMapper.GetMappedStatement(statementName);
            ISqlMapSession session = new SqlMapSession(sqlMapper);

            if (sqlMapper.LocalSession != null)
            {
                session = sqlMapper.LocalSession;
            }
            else
            {
                session = sqlMapper.OpenConnection();
            }

            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);
            mapStatement.PreparedCommand.Create(request, session as ISqlMapSession, statement, paramObject);
            IDbCommand cmd = session.CreateCommand(CommandType.Text);
            cmd.CommandText = request.IDbCommand.CommandText;
            return cmd;
        }
        #endregion
    }
}
