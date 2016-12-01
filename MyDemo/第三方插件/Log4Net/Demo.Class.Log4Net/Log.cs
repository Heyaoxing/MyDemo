using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Class.Log4Net
{
    public class Log 
    {
        /// <summary>
        /// 记录类型为[Info]的日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            GetLogger().Info(GetSrc(new StackTrace()) + message);
        }

        /// <summary>
        /// 记录类型为[Debug]的日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            GetLogger().Debug(GetSrc(new StackTrace()) + message);
        }

        /// <summary>
        /// 记录类型为[Warning]的日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Warning(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            GetLogger().Warning(GetSrc(new StackTrace()) + message);

        }

        /// <summary>
        /// 记录类型为[Error]的日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Error(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            GetLogger().Error(GetSrc(new StackTrace()) + message);

        }

        /// <summary>
        /// 记录异常日志信息
        /// </summary>
        /// <param name="exception">异常信息</param>
        public static void Exception(Exception exception)
        {
            if (exception == null)
                return;

            GetLogger().Exception(exception);

            //如有内部异常，也记录
            if (exception.InnerException != null)
            {
                GetLogger().Exception(exception.InnerException);
            }
        }

        private static ILog GetLogger()
        {
            return new Log4netLogger();
        }


        /// <summary>
        /// 获取日志记录所在位置
        /// </summary>
        /// <param name="trace"></param>
        /// <returns></returns>
        private static string GetSrc(StackTrace trace)
        {
            if (trace != null)
            {
                var methodBase = trace.GetFrame(1).GetMethod();
                if (methodBase != null)
                {
                    string src = string.Format(" {0}.{1}: ", methodBase.DeclaringType.FullName, methodBase.Name);
                    return src;
                }
            }
            return string.Empty;
        }
    }
}
