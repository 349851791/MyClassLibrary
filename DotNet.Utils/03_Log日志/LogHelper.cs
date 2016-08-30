/// <summary>
/// 使用前提： 
/// 类 说 明:   日志类
/// 编 码 人:   张贺
/// 创建日期:   2015-03-11
/// 更新记录:   2015-10-13 
///             1.增加两个参数为Exception的方法
///             2015-10-22
///             1.将原来的命名空间DotNet.Utils.Log修改为DotNet.Utils 
///             2.修改此类为静态
///              2015-11-4
///             1.增加异常输出sql语句
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Utils
{
    public static class LogHelper
    {
        private static LogManage logManager;
        static LogHelper()
        {
            logManager = new LogManage();
        }

        /// <summary>
        /// 直接输出传入信息
        /// </summary>
        /// <param name="msg">要输出的信息</param>
        public static void WriteLog(string msg)
        {
            logManager.WriteLog(msg + "\r\n");
        }

        /// <summary>
        /// 输出日志以及日志类型
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志信息</param>
        public static void WriteLog(LogFile logFile, string msg)
        {
            logManager.WriteLog(logFile, msg + "\r\n");
        }

        /// <summary>
        /// 输出日志以及日志类型
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志信息</param>
        /// <param name="stackTrace">堆栈信息</param>
        public static void WriteLog(LogFile logFile, string msg, string stackTrace)
        {
            logManager.WriteLog(logFile, "日志信息:" + msg + "\r\n堆栈信息:" + stackTrace + "\r\n");
        }

        /// <summary>
        /// 输出错误信息和堆栈信息
        /// </summary>
        /// <param name="ex">异常对象</param> 
        public static void WriteLog(Exception ex)
        {
            logManager.WriteLog("错误信息:" + ex.Message + "\r\n堆栈:" + ex.StackTrace + "\r\n");
        }

        /// <summary>
        /// 输出错误信息和堆栈信息
        /// </summary>
        /// <param name="ex">异常对象</param> 
        /// <param name="sql">sql语句</param>
        public static void WriteLog(Exception ex, string sql)
        {
            logManager.WriteLog("错误信息:" + ex.Message + "\r\nSQL语句:" + sql + "\r\n堆栈:" + ex.StackTrace + "\r\n");
        }

        /// <summary>
        /// 输出日志以及日志类型
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志信息</param>
        /// <param name="stackTrace">堆栈信息</param>
        public static void WriteLog(LogFile logFile, Exception ex)
        {
            logManager.WriteLog(logFile, "日志信息:" + ex.Message + "\r\n堆栈信息:" + ex.StackTrace + "\r\n");
        }
    }


    public enum LogFile
    {
        Error,
        SQL,
        Info
    }
}
