using System;
using SSPU_SmartLockProcService.BLL.Pakages;


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
