using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SSPU_SmartLockProcService.BLL.ServerEventArgs
{
    public class MqttSubscribedEventArgs:EventArgs
    {
        public ushort messageId;
        public string[] SubscribedTopics;

        public MqttSubscribedEventArgs(ushort msgid)
        {
            SubscribedTopics = MqttTopic.topic;
        }
    }
}
