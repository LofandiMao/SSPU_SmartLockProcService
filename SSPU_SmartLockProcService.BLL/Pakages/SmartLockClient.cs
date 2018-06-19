using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SSPU_SmartLockProcService.BLL.Pakages
{
    class SmartLockClient
    {        
        /// <summary>
        /// 数据包ID
        /// </summary>
        public string PackageID { get; set; }
        /// <summary>
        /// 子设备唯一标识ID
        /// </summary>
        public string IotID { get; set; }
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// PK 客户代码
        /// </summary>
        public string PK { get; set; }
        /// <summary>
        /// 设备密钥
        /// </summary>
        public string DeviceSecret { get; set; }
        /// <summary>
        /// 与子设备绑定的用户ID（APP id）
        /// </summary>
        public string BindID { get; set; }

        private string _salt;



        public void GetDeviceSecret(string salt,string id)
        {
            var hMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(salt));
            var dataBuffer = Encoding.UTF8.GetBytes(id);
            var hashBytes = hMACSHA1.ComputeHash(dataBuffer);

            DeviceSecret = Convert.ToBase64String(hashBytes);
            _salt = salt;
        }
    }
}
