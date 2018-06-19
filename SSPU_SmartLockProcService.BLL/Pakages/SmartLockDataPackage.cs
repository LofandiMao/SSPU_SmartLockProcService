using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SSPU_SmartLockProcService.BLL.Pakages
{
    class SmartLockDataPackage
    {
        public SmartLockDataPackage(byte[] package)
        {
            try
            {
                string msgString = Encoding.Default.GetString(package);
                JObject msgJson = JObject.Parse(msgString);
            }
            catch
            {
                throw new Exception("Data包解析错误，数据可能不是JSON格式!");
            }
        }
    }

    class SmartLockEventPackage
    {
        public SmartLockEventPackage(byte[] package)
        {
            try
            {
                string msgString = Encoding.Default.GetString(package);
                JObject msgJson = JObject.Parse(msgString);
            }
            catch
            {
                throw new Exception("Event包解析错误，数据可能不是JSON格式!");
            }
        }
    }

    class SmartLockRespondPackage
    {
        public SmartLockRespondPackage(byte[] package)
        {
            try
            {
                string msgString = Encoding.Default.GetString(package);
                JObject msgJson = JObject.Parse(msgString);
            }
            catch
            {
                throw new Exception("Respond包解析错误，数据可能不是JSON格式!");
            }
        }
    }
}
