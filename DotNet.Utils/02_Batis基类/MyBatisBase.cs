/// <summary>
/// 使用前提： 
/// 类 说 明： batis的基类,已过时
/// 编 码 人： 张贺
/// 创建日期： 2015-10-14
/// 更新日期： 
/// 更新内容:
/// </summary>
using System;
using System.Collections.Generic; 
using IBatisNet.DataMapper;
using System.Data;
using System.Reflection;

namespace DotNet.Utils
{
     static class MyBatisBase 
    {
        #region 基本增删改查操作
        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="statementName">声明名称</param>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public static int Insert<T>(string statementName, T t)
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
        public static int Delete(string statementName, int primaryKeyId)
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
        public static int Update<T>(string statementName, T t)
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
        public static T Get<T>(string statementName, int primaryKeyId) where T : class
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
        public static IList<T> QueryForList<T>(string statementName, object parameterObject = null)
        {
            ISqlMapper iSqlMapper = Mapper.Instance();
            if (iSqlMapper != null)
            {
                return iSqlMapper.QueryForList<T>(statementName, parameterObject);
            }
            return null;
        }
        #endregion 
    }
}
