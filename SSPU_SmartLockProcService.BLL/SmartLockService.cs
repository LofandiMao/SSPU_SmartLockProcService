using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using uPLibrary.Networking.M2Mqtt;
using SSPU_SmartLockProcService.BLL.ServerEventArgs;

namespace SSPU_SmartLockProcService.BLL
{
    public class SmartLockService
    {
        /// <summary>
        /// MQTT服务器地址
        /// </summary>
        public string MqttServerHost
        {
            get { return _host; }
            set { value = _host; }
        }
        /// <summary>
        /// MQTT服务器端口
        /// </summary>
        public int MqttServerPort
        {
            get { return _port; }
            set { value = _port; }
        }
        /// <summary>
        /// MQTT用户名
        /// </summary>
        public string MqttServerUser
        {
            get { return _user; }
            set { value = _user; }
        }
        /// <summary>
        /// MQTT密码
        /// </summary>
        public string MqttServerPwd
        {
            get { return _password; }
            set { value = _password; }
        }
        //证书文件路径
        private string cerFile = Environment.CurrentDirectory + "\\root.cer";

        private readonly string _host = "39.105.111.94";
        private readonly int _port = 1883;
        private readonly string _user = "admin";
        private readonly string _password = "public";
        private readonly string _id = "admin";

        /// <summary>
        /// 启动MQTT服务
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            bool isConnected = false;
            try
            {

                X509Certificate x509Certificate = new X509Certificate(cerFile);

                try
                {
                    MqttClient client = new MqttClient(_host, _port, false,
                                                        x509Certificate, null,
                                                        MqttSslProtocols.TLSv1_2,
                                                        new System.Net.Security.RemoteCertificateValidationCallback(CafileValidCallback));


                    client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                    client.MqttMsgUnsubscribed += Client_MqttMsgUnsubscribed;
                    client.MqttMsgPublished += Client_MqttMsgPublished;
                    client.MqttMsgSubscribed += Client_MqttMsgSubscribed;
                    client.ConnectionClosed += Client_ConnectionClosed;

                    client.Connect(_id, _user, _password);

                    if (client.IsConnected)
                    {;
                        client.Subscribe(MqttTopic.topic, MqttTopic.qoslevel);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return isConnected;
            }
            catch (Exception ex)
            {
                throw new Exception("启动MQTT服务出错！" + ex.Message);
            }
        }

        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Client_MqttMsgSubscribed(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgSubscribedEventArgs e)
        {
            MqttSubscribedEventArgs eventArgs = new MqttSubscribedEventArgs(e.MessageId);

            RaiseMqttSubscribed(eventArgs);
        }

        private void Client_MqttMsgPublished(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishedEventArgs e)
        {
            
        }

        private void Client_MqttMsgUnsubscribed(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgUnsubscribedEventArgs e)
        {
            
        }

        private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {

            switch (e.Topic)
            {
                case MqttTopic.RegisterTopic:
                    SmartLockRegister smartLock = new SmartLockRegister(e.Message);
                    byte[] resArray = smartLock.PackageResponse();
                    MqttClient client = (MqttClient)sender;
                    string resTopic = MqttTopic.GetClientTopic("SYS", smartLock.SmartLockClient.IotID,"REG");
                    client.Publish(resTopic, resArray);
                    break;
                case MqttTopic.EventTopic:
                    break;
                case MqttTopic.RespondTopic:
                    break;
                case MqttTopic.DataTopic:
                    break;
            }

        }

        private bool CafileValidCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            string msg = "X509 链状态:";
            foreach (X509ChainStatus status in chain.ChainStatus)
            {
                msg += status.StatusInformation + "\n";
            }
            msg += "SSL策略问题：" + (int)sslPolicyErrors;

            //Console.WriteLine(msg);

            if (sslPolicyErrors != SslPolicyErrors.None)
                return false;
            return true;
        }

        /// <summary>
        /// 定阅成功事件
        /// </summary>
        public event EventHandler<MqttSubscribedEventArgs> MqttSubscribed;

        private void RaiseMqttSubscribed(MqttSubscribedEventArgs eventArgs)
        {
            MqttSubscribed?.Invoke(this, new MqttSubscribedEventArgs(eventArgs.messageId));
        }

        /// <summary>
        /// 断开连接事件
        /// </summary>
        public event EventHandler<MqttDisconnectEventArgs> MqttDisconnect;

        private void RaiseMqttDisconnect(MqttDisconnectEventArgs eventArgs)
        {
            MqttDisconnect?.Invoke(this, new MqttDisconnectEventArgs());
        }

        /// <summary>
        /// 发布成功事件
        /// </summary>
        public event EventHandler<MqttMsgPublishedEventArgs> MqttPublished;

        private void RaiseMqttPublished(MqttMsgPublishedEventArgs eventArgs)
        {
            MqttPublished?.Invoke(this, new MqttMsgPublishedEventArgs());
        }
    }
}
