using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTnet.Server;

namespace SSPU_SmartLockTestService
{
    public partial class ServicForm : Form
    {
        private Dictionary<string,ConnectedMqttClient> mqttClients = new Dictionary<string,ConnectedMqttClient>();

        private delegate void AppendTextToRtbDelegate(string Text);  //声明委托类型
        private delegate void SetTextTb(ConnectedMqttClient client,bool flag);

        

        public ServicForm()
        {
            InitializeComponent();

            StartMqttServer();

            
        }

        private async void StartMqttServer()
        {
            var mqttServer = new MQTTnet.MqttFactory().CreateMqttServer();

            var optionsBuilder = new MqttServerOptionsBuilder()
                                    .WithConnectionBacklog(100)
                                    .WithDefaultEndpointPort(1883); 

            mqttServer.ClientConnected += MqttServer_ClientConnected;
            mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;


            await mqttServer.StartAsync(optionsBuilder.Build());
        }

        private void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            ConnectedMqttClient client = e.Client;
            mqttClients.Remove(client.ClientId);

            Invoke(new SetTextTb(SetMessageByClientMsg), client,true);
        }

        private void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            ConnectedMqttClient client = e.Client;
            mqttClients.Add(client.ClientId, client);

            Invoke(new SetTextTb(SetMessageByClientMsg), client,false);
        }

        private void SetMessageByClientMsg(ConnectedMqttClient client,bool flag)
        {
            tbClientCount.Text = mqttClients.Count().ToString();
            if (flag)
            {
                rtbLog.AppendText("子设备：" + client.ClientId + "已离线!\r\n");
            }
            else
            {
                rtbLog.AppendText("子设备：" + client.ClientId + "已上线!\r\n");
            }

            listBox1.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                if (i >= mqttClients.Count) break;
                listBox1.Items.Add(mqttClients.Keys.ToArray()[i].ToString());
            }   
        }


    }
}
