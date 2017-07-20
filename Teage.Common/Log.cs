using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Common
{
    public class Log
    {
        public static void Error(string msg)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Error(msg);
        }
        public static void Error(string msg, Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Error(msg, ex);
        }
        public static void Info(string msg)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Info(msg);
        }
        public static void Info(string msg, Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Info(msg, ex);
        }
        public static void Debug(string msg)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Debug(msg);
        }
        public static void Debug(string msg, Exception ex)
        {
            LogManager.GetLogger(GetCurrentMethodFullName()).Debug(msg, ex);
        }

        #region 私有方法

        /// <summary>
        /// 获取方法名
        /// </summary>
        /// <returns></returns>
        private static string GetCurrentMethodFullName()
        {
            try
            {
                StackFrame frame;
                string str;
                int num = 2;
                StackTrace trace = new StackTrace();
                int length = trace.GetFrames().Length;
                do
                {
                    frame = trace.GetFrame(num++);
                    str = frame.GetMethod().DeclaringType.ToString();
                }
                while (str.EndsWith("Excption") && (num < length));
                string name = frame.GetMethod().Name;
                return (str + "." + name);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
