using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace CommonLibs
{
    public class ErrorLog
    {
        private static string LogName = "YoloTrainingTool";
        private static string LogFile = "YoloTrainingTool.txt";
        //MB
        private const int LogFileSize = 10;

        /// <summary>
        ///     オペレイションのログ内容を書込
        /// </summary>
        /// <param name="strOperation">オペレイションの操作</param>
        /// <remarks></remarks>
        public static void WriteOperation(string strOperation)
        {
            var strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "---------------" + "  " +
                                  "\r\n" + strOperation;

            WriteLog(strLogString);
        }

        /// <summary>
        ///     オペレイションのログ内容を書込
        /// </summary>
        /// <param name="strObjectName">オペレイションのオブジェクト</param>
        /// <param name="strOperation">オペレイションの操作</param>
        /// <remarks></remarks>
        public static void WriteOperation(string strObjectName, string strOperation)
        {
            var strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "---------------" + "\r\n" +
                                  "  " + strObjectName + "\r\n" + strOperation;

            WriteLog(strLogString);
        }

        /// <summary>
        ///     エラーのログ内容を書込
        /// </summary>
        /// <param name="ex"></param>
        /// <remarks></remarks>
        public static void WriteError(Exception ex)
        {
            var strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss") + "---------------" +
                               "\r\n" + "Error：" + "\r\n";
            strLogString += ex.Message + "\r\n";
            strLogString += ex.StackTrace;
            WriteLog(strLogString);
        }

        /// <summary>
        ///     エラーのログ内容を書込
        /// </summary>
        /// <param name="strError">エラーのメッセージ</param>
        /// <remarks></remarks>
        public static void WriteError(string strError)
        {
            var strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "---------------" +
                               "\r\n" + "Error:" + "\r\n";
            strLogString += strError;
            WriteLog(strLogString);
        }

        /// <summary>
        ///     ログ内容を書込
        /// </summary>
        /// <param name="logContent">ログ内容</param>
        /// <remarks></remarks>
        public static void WriteLog(string logContent)
        {
            //strLogDir = SystemConfig.AppRoot;
            var strLogDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +"\\Logs";
            if (!Directory.Exists(strLogDir)) Directory.CreateDirectory(strLogDir);
            if (!strLogDir.EndsWith("\\"))
                strLogDir += "\\";
            LogFile = LogName+"_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".txt";
            var strFileName = strLogDir + LogFile;

            try
            {
                var charset = Encoding.GetEncoding("UTF-8");
                if (!File.Exists(strFileName))
                {
                    var oFile = File.Create(strFileName);
                    var oReader = new StreamWriter(oFile, charset);
                    oReader.WriteLine(logContent);
                    oReader.Close();
                    oFile.Close();
                }
                else
                {
                    // Append text in file when file exitsed
                    var oFile1 = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    if (oFile1.Length > LogFileSize * (1024 * 1024))
                    {
                        oFile1.Close();
                        File.Delete(strFileName);
                    }
                    else
                    {
                        oFile1.Close();
                    }
                    var oReader = new StreamWriter(strFileName, true, charset);
                    oReader.WriteLine(logContent);
                    oReader.Close();
                }
            }
            catch (Exception)
            {
                // Throw ex
            }
        }
    }
}