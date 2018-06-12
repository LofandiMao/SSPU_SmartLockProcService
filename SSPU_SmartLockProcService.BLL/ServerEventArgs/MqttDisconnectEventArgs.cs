using System;
using uPLibrary.Networking.M2Mqtt;

namespace SSPU_SmartLockProcService.BLL.ServerEventArgs
{
    public class MqttDisconnectEventArgs:EventArgs
    {
        public MqttClient GetClient;


    }
}
