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
            try
            {
                lockService.Start();
            }
            catch (Exception ex)
            {
                rtbLog.AppendText(ex.Message + "\r\n");
            }
        }

        private void LockService_MqttSubscribed(object sender, BLL.ServerEventArgs.MqttSubscribedEventArgs e)
        {
            string text = "订阅成功！服务器订阅topic：{";

            for (int i = 0; i < e.SubscribedTopics.Length; i++)
            {
                text += e.SubscribedTopics[i] + ",";
            }
            text += "}\r\n";

            Invoke(new AppendTextToRtbDelegate(AppedRichTextBox), text); //线程通过方法的委托执行showStuIfo()，实现对ListBox控件的访问
        }

        private void AppedRichTextBox(string text)
        {
            rtbLog.Text += DateTime.Now.ToLongTimeString() + ":" + text;
        }
    }
}
