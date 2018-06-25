using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSPU_SmartLockProcService.BLL;
using SSPU_SmartLockProcService.DAL;

namespace SSPU_SmartLockProcService
{
    public partial class SSPU_mainForm : Form
    {
        public delegate void AppendTextToRtbDelegate(string Text);  //声明委托类型

        public SSPU_mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SmartLockService lockService = new SmartLockService();

            lockService.MqttSubscribed += LockService_MqttSubscribed;
            lockService.MqttErroOccurred += LockService_MqttErroOccurred;
            try
            {
                lockService.Start();
            }
            catch (Exception ex)
            {
                rtbLog.AppendText(ex.Message + "\r\n");
                Logs.Error(ex.Message);
            }
        }

        private void LockService_MqttErroOccurred(object sender, BLL.ServerEventArgs.MqttErroEventArgs e)
        {
            string text = e.ErroID + "发生错误：" + e.ErroString + "\r\n";
            Logs.Error(e.ErroString);
            Invoke(new AppendTextToRtbDelegate(AppedRichTextBox), text);
        }

        private void LockService_MqttSubscribed(object sender, BLL.ServerEventArgs.MqttSubscribedEventArgs e)
        {
            string text = "订阅成功！服务器订阅topic：{";

            for (int i = 0; i < e.SubscribedTopics.Length; i++)
            {
                text += e.SubscribedTopics[i] + ",";
            }
            text += "}\r\n";

            Logs.Log(text);

            Invoke(new AppendTextToRtbDelegate(AppedRichTextBox), text); 
        }

        private void AppedRichTextBox(string text)
        {
            rtbLog.Text += DateTime.Now.ToLongTimeString() + ":" + text;
            
        }
    }
}
