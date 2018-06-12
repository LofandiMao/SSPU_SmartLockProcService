using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SSPU_SmartLockProcService.BLL;

namespace SSPU_SmartLockProcService.BLL
{
    class SmartLockRegister
    {
        /// <summary>
        /// 数据包ID
        /// </summary>
        public string PackageID { get { return _packageID; } set { value = _packageID; } }
        /// <summary>
        /// 固件版本
        /// </summary>
        public string FwVer { get { return _fwVer; } set { value = _fwVer; } }
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string DeviceName { get { return _deviceName; } set { value = _deviceName; } }
        /// <summary>
        /// PK 客户代码
        /// </summary>
        public string PK { get { return _productKey; } set { value = _productKey; } }
        /// <summary>
        /// 包方法名，阿里云兼容，未使用
        /// </summary>
        public string Mothod { get { return _mothod; } set { value = _mothod; } }

        public SmartLockClient SmartLockClient = new SmartLockClient();

        private readonly string _packageID;
        private readonly string _fwVer;
        private readonly string _deviceName;
        private readonly string _productKey;
        private readonly string _mothod;

        /// <summary>
        /// 从package中获得client相关参数
        /// </summary>
        /// <param name="package"></param>
        public SmartLockRegister(byte[] package)
        {
            try
            {
             

                string msgString = Encoding.Default.GetString(package);
                JObject msgJson = JObject.Parse(msgString);

                _packageID = msgJson["id"].ToString();
                _fwVer = msgJson["version"].ToString();
                _mothod = msgJson["method"].ToString();
                //参数
                JObject para = JObject.FromObject(msgJson["params"][0]);
                _deviceName = para["deviceName"].ToString();
                _productKey = para["productKey"].ToString();

                SmartLockParsePackage();
            }
            catch
            {
                throw new Exception("注册包解析错误，数据可能不是JSON格式!");
            }
        }

        private void SmartLockParsePackage()
        {
            SmartLockClient.DeviceName = _deviceName;
            SmartLockClient.PK = _productKey;
            SmartLockClient.IotID = _deviceName;  //deviceID可以单独生成，也可以使用_deviceName
            SmartLockClient.PackageID = _packageID;
            SmartLockClient.BindID = "admin";     //绑定admin 共享模式，单用户模式可以绑定相应的用户ID

            try
            {
                SmartLockClient.GetDeviceSecret(SmartLockClient.BindID, SmartLockClient.IotID); //获得密钥
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 生成回复包
        /// </summary>
        /// <returns>response</returns>
        public byte[] PackageResponse()
        {
            JObject package = new JObject();
            try
            {
                package.Add("id", _packageID);
                int code = SamrtLockBindVerify(SmartLockClient.PK, SmartLockClient.IotID);
                package.Add("code", code);
                //生成数据组
                JObject data = new JObject();
                data.Add("iotId", SmartLockClient.IotID);
                data.Add("productKey", SmartLockClient.PK);
                data.Add("deviceName", SmartLockClient.DeviceName);
                data.Add("deviceSecret", SmartLockClient.DeviceSecret);
                data.Add("bindID", SmartLockClient.BindID);
      
                JArray ja = new JArray(data);

                package.Add("data", ja);

                return Encoding.Default.GetBytes(package.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int SamrtLockBindVerify( string pk, string id)
        {
            //通过录入数据库检查子设备ID是否属于该项目。
            //响应代码 200 :成功 460：参数错误 401：设备ID与PK不一致 
            int code = 200;

            return code;
        }
        /// <summary>
        /// 将需要注册的子设备信息写入数据库
        /// </summary>
        /// <param name="client">子设备信息</param>
        /// <returns></returns>
        public bool SmartLockSaveRegisterClient(SmartLockClient client)
        {

            return true;
        }

    }
}
