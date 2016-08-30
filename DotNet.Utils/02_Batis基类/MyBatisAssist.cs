/// <summary>
/// 使用前提： 
/// 类 说 明： batis的辅助类,已过时
/// 编 码 人： 张贺
/// 创建日期： 2015-10-14
/// 更新日期： 
/// 更新内容:
/// </summary>
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration.Statements;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utils
{
     static class MyBatisAssist
    {
        /// <summary>
        /// 获取动态生成的sql语句
        /// </summary>
        /// <param name="stateMentName">声明名称</param>
        /// <param name="paramObject">参数</param>
        /// <returns></returns>
        public static string GetSql(string stateMentName, object paramObject)
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
        public static DataTable QueryForDataDataTable(string statementName, object paramObject = null, string tableName = null, int tableIndex = 0)
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
        private static IDbCommand GetDbCommand(ISqlMapper sqlMapper, string statementName, object paramObject)
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
    }
}
