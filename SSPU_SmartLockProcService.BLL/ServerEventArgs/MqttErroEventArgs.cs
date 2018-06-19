using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPU_SmartLockProcService.BLL.ServerEventArgs
{
    public class MqttErroEventArgs : EventArgs
    {
        /// <summary>
        /// 错误详情
        /// </summary>
        public string ErroString { get; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErroID {get;}
        
        public MqttErroEventArgs(string id, string Message)
        {
            ErroString = id;
            ErroID = Message;
        }
    }
}
