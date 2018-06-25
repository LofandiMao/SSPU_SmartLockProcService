using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPU_SmartLockProcService.DAL
{
    static public class Logs
    {
        static public int Error(string errorStr)
        {
            try
            {
                string logFilePath = Environment.CurrentDirectory + "\\log\\" + DateTime.Now.Date.ToString("yy-MM-dd") + "\\error.log";

                if (!File.Exists(logFilePath))
                {
                    FileInfo fileInfo = new FileInfo(logFilePath);
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }
               
                Stream stream = new FileStream(logFilePath, FileMode.OpenOrCreate);
                stream.Seek(0, SeekOrigin.End);
                StreamWriter writer = new StreamWriter(stream);
                
                writer.WriteLine(DateTime.Now.ToString() + ":" + errorStr);
                writer.Flush();
                writer.Close();

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        static public int Log(string logStr)
        {
            try
            {
                string logFilePath = Environment.CurrentDirectory + "\\log\\" + DateTime.Now.Date.ToString("yy-MM-dd") + "\\log.log";

                if (!File.Exists(logFilePath))
                {
                    FileInfo fileInfo = new FileInfo(logFilePath);
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }

                Stream stream = new FileStream(logFilePath, FileMode.OpenOrCreate);
                stream.Seek(0, SeekOrigin.End);
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(DateTime.Now.ToString() + ":" + logStr);
                writer.Flush();
                writer.Close();

                return 0;
            }
            catch
            {
                return -1;
            }
        }

        static public int Event(string eventStr)
        {
            try
            {
                string logFilePath = Environment.CurrentDirectory + "\\log\\" + DateTime.Now.Date.ToString("yy-MM-dd") + "\\eventStr.log";

                if (!File.Exists(logFilePath))
                {
                    FileInfo fileInfo = new FileInfo(logFilePath);
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }

                Stream stream = new FileStream(logFilePath, FileMode.OpenOrCreate);
                stream.Seek(0, SeekOrigin.End);
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(DateTime.Now.ToString() + ":" + eventStr);
                writer.Flush();
                writer.Close();

                return 0;
            }
            catch
            {
                return -1;
            }
        }

    }
}
