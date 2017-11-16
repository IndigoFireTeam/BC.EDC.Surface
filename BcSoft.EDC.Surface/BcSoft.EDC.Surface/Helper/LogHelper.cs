using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BcSoft.EDC.Surface.Helper
{
    public class LogHelper
    {
        private static object LockObject = new object();

        private enum LogLevel
        {
            Info,
            Error
        }

        private static string LogPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"\Logs\";
            }
        }

        public static void WriteErrorLog(string functionName,string errorDescription)
        {
            var logMessage = string.Format("函数名称:{0} 错误信息:{1}", functionName, errorDescription);

            WriteLog(LogLevel.Error.ToString(), logMessage);
        }

        #region Private Methods
        private static void WriteLog(string prefix, string message)
        {
            lock (LockObject)
            {
                var fileName = string.Format("{0}_{1}.log", prefix, DateTime.Now.ToString("yyyyMMdd"));
                var filePath = LogPath + fileName;

                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);

                FileStream fs = null;
                if (!File.Exists(filePath))
                {
                    fs = File.Create(filePath);
                }
                else
                {
                    fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\r\n");
                fs.Write(buffer, 0, buffer.Length);

                fs.Dispose();
            }
        }
        #endregion
    }
}
