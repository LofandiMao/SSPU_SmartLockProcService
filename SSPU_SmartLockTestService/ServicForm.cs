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

            await mqttServer.StartAsync(optionsBuilder.Build());

            //await mqttServer.StopAsync();
        }

        private void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            
        }
    }
}
