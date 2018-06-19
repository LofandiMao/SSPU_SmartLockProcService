using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SSPU_SmartLockProcService.BLL.Pakages
{
    class MqttTopic
    {
        /// <summary>
        /// 客户端注册设备的topic，服务器订阅
        /// </summary>
        public const  string RegisterTopic = "/SYS/admin/REG";
        /// <summary>
        /// 客户端上报事件的topic,服务器订阅
        /// </summary>
        public const string EventTopic = "/SYS/admin/EVENT";
        /// <summary>
        /// 客户端上报数据的topic,服务器订阅
        /// </summary>
        public const string DataTopic = "/SYS/admin/DATA";
        /// <summary>
        /// 客户端回复服务器操作结果的报文,服务器订阅
        /// </summary>
        public const string RespondTopic = "/SYS/admin/RES";
        /// <summary>
        /// 根据参数拼接topic
        /// </summary>
        /// <param name="productKey">客户代码或产品代码，集群通讯时备用</param>
        /// <param name="deviceID">设备ID</param>
        /// <param name="method">topic类型 REG=注册，EVENT=事件，DATA=上报数据,RES=回复，CONFIG=设备参数，QUERY=查询</param>
        /// <returns></returns>
        static public string GetClientTopic(string productKey, string deviceID, string method)
        {
            //关于productKey的说明，目前productKey只有一个，固定为"SYS"。
            //deviceID 设备代码，即该topic的目标终端。
            return "/" + productKey + "/" + deviceID + "/" + method;
        }
        /// <summary>
        /// 订阅topic的质量等级
        /// </summary>
        static public readonly byte[] qoslevel = {
                                            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE,
                                            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE,
                                            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE,
                                            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE
                        };
        /// <summary>
        /// 订阅的topic 
        /// </summary>
        static public readonly string[] topic = {
                                            EventTopic,
                                            RegisterTopic,
                                            DataTopic,
                                            RespondTopic
                        };

    }
}
