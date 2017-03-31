using com.dasudian.iot.sdk;
using DataHubDemo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataHubDemo
{
    public partial class DataHubDemo : Form
    {
        public DataHubDemo()
        {
            InitializeComponent();
        }
        private static Image PICTURE_OK = Resources.ok;
        private static Image PICTURE_NG = Resources.ng;
        private DataHubClient client = null;
        // 用于在接收消息时刷新UI的代理函数
        private delegate void MessageDelegate(string message);
        private delegate void ConnectionStatusChangedDelegate(bool isConnected);

        private void MessageReceived(string message)
        {
            this.messageReceive.Text = message;
        }

        private void ConnectionStatusChanged(bool isConnected)
        {
            if (isConnected)
            {
                this.picture_connect_status.Image = PICTURE_OK;
            }
            else
            {
                this.picture_connect_status.Image = PICTURE_NG;
            }
        }

        private void button_connect(object sender, EventArgs e)
        {
            // 下面的服务器地址、instanceId和instanceKey为大数点公有云测试服务器地址。
            // 注意：下面的服务器地址仅仅用于作为demo和简单测试使用，在正式使用大数点datahub服务时，
            // 请联系大数点客服人员(www.dasudian.com)，获取私有的服务器地址、instanceId和instanceKey。
            string serverURL = this.serverURL.Text;
            string instanceId = this.instanceId.Text;
            string instanceKey = this.instanceKey.Text;
            string clientName = null;
            if (this.checkBox_random_clientname.Checked)
            {
                clientName = Guid.NewGuid().ToString();
            }
            else
            {
                clientName = this.textBox_userName.Text;
            }
             

            string clientId = null;
            if (this.checkBox_clientid.Checked)
            {
                clientId = Guid.NewGuid().ToString();
            }
            else
            {
                clientId = this.textBox_clientId.Text;
            }
            if (clientId == null || clientId.Length == 0 ||
                clientName == null || clientName.Length == 0 ||
                instanceId == null || instanceId.Length == 0 ||
                instanceKey == null || instanceKey.Length == 0)
            {
                MessageBox.Show("clientId，clientName，instanceId，instanceKey不能为空");
                return;
            }

            client = new DataHubClient.Builder(instanceId, instanceKey, clientName, clientId)
                .SetServerURL(serverURL).Build();
            client.MessageReceived += client_MessageReceived;
            client.ConnectionStatusChanged += client_ConnectionStatusChanged;
        }

        void client_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs e)
        {
            if (this.InvokeRequired)// 判断是否是UI线程
            {
                ConnectionStatusChangedDelegate d = new ConnectionStatusChangedDelegate(this.ConnectionStatusChanged);
                this.Invoke(d, e.IsConnected);
            }
            else
            {
                if (e.IsConnected)
                {
                    this.picture_connect_status.Image = PICTURE_OK;
                }
                else
                {
                    this.picture_connect_status.Image = PICTURE_NG;
                }
            }
        }

        /// <summary>
        /// 订阅一个topic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_subscribe(object sender, EventArgs e)
        {
            if (client == null)
            {
                MessageBox.Show("请先创建客户端实例");
                return;
            }
            string topic = this.text_sub_or_unsub_qos.Text;
            int input_qos = Int32.Parse(this.text_sub_qos.Text);
            if (topic == null || input_qos < 1 || input_qos > 3)
            {
                MessageBox.Show("错误的qos或topic");
                return;
            }
            byte qos;
            if (input_qos == 1)
            {
                qos = DataHubClient.QOS_LEVEL_AT_MOST_ONCE;
            }
            else if (input_qos == 2)
            {
                qos = DataHubClient.QOS_LEVEL_AT_LEAST_ONCE;
            }
            else
            {
                qos = DataHubClient.QOS_LEVEL_EXACTLY_ONCE;
            }
            
            int ret = client.Subscribe(topic, qos);
            if (ret == Constants.ERROR_NONE)
            {
                MessageBox.Show("Subscribe success");
            }
            else
            {
                MessageBox.Show("Subscribe failed:" + ret);
            }
        }

        void client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (this.checkBox_save_tofile.Checked)
            {
                using (FileStream fs = new FileStream("message.txt", FileMode.OpenOrCreate))
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.BaseStream.Seek(0, SeekOrigin.End);
                    w.WriteLine("topic:" + e.Topic);
                    w.WriteLine("payload:" + Encoding.UTF8.GetString(e.Message));
                    w.Flush();
                    w.Close();
                }
            }

            if (this.InvokeRequired)// 判断是否是UI线程
            {
                MessageDelegate d = new MessageDelegate(this.MessageReceived);
                this.Invoke(d, Encoding.UTF8.GetString(e.Message));
            }
            else
            {
                this.messageReceive.Text = Encoding.UTF8.GetString(e.Message);
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.Destroy();
            }
        }

        private void unsubscribe_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                MessageBox.Show("请先创建客户端实例");
                return;
            }
            string topic = this.text_sub_or_unsub_qos.Text;
            if (topic == null)
            {
                MessageBox.Show("topic不能为null");
                return;
            }
            int ret = client.Unsubscribe(topic, 10);
            if (ret == Constants.ERROR_NONE)
            {
                MessageBox.Show("Unsubscribe success");
            }
            else
            {
                MessageBox.Show("Unsubscribe failed:" + ret);
            }
        }

        private void publish_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                MessageBox.Show("请先创建客户端实例");
                return;
            }
            string topic = this.text_pub_topic.Text;
            int input_qos = Int32.Parse(this.text_pub_qos.Text);
            string payload = this.message_pub.Text;
            if (topic == null || payload == null)
            {
                MessageBox.Show("topic或消息内容不能为null");
                return;
            }
            if (input_qos < 1 || input_qos > 3)
            {
                MessageBox.Show("qos只能是1、2、3");
                return;
            }
            byte qos;
            if (input_qos == 1)
            {
                qos = DataHubClient.QOS_LEVEL_AT_MOST_ONCE;
            }
            else if (input_qos == 2)
            {
                qos = DataHubClient.QOS_LEVEL_AT_LEAST_ONCE;
            }
            else
            {
                qos = DataHubClient.QOS_LEVEL_EXACTLY_ONCE;
            }
            com.dasudian.iot.sdk.Message message = new com.dasudian.iot.sdk.Message();
            message.payload = Encoding.UTF8.GetBytes(payload);
            int messageId;
            client.Publish(topic, message, qos, out messageId);
        }


        /// <summary>
        /// 同步发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendrequest_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                MessageBox.Show("请先创建客户端实例");
                return;
            }
            string topic = this.text_pub_topic.Text;
            int input_qos = Int32.Parse(this.text_pub_qos.Text);
            string payload = this.message_pub.Text;
            if (topic == null || payload == null)
            {
                MessageBox.Show("topic或消息内容不能为null");
                return;
            }
            if (input_qos < 1 || input_qos > 3)
            {
                MessageBox.Show("qos只能是1、2、3");
                return;
            }
            byte qos;
            if (input_qos == 1)
            {
                qos = DataHubClient.QOS_LEVEL_AT_MOST_ONCE;
            }
            else if (input_qos == 2)
            {
                qos = DataHubClient.QOS_LEVEL_AT_LEAST_ONCE;
            }
            else
            {
                qos = DataHubClient.QOS_LEVEL_EXACTLY_ONCE;
            }
            com.dasudian.iot.sdk.Message message = new com.dasudian.iot.sdk.Message();
            message.payload = Encoding.UTF8.GetBytes(payload);
            int ret = client.SendRequest(topic, message, qos, 10000);
            if (ret == 0)
            {
                MessageBox.Show("SendRequest success");
            }
            else
            {
                MessageBox.Show("SendRequest failed:" + ret);
            }
        }

        private void checkBox_random_clientname_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_random_clientname.Checked)
            {
                this.textBox_userName.ReadOnly = true;
            }
            else
            {
                this.textBox_userName.ReadOnly = false;
            }
        }

        private void checkBox_clientid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_clientid.Checked)
            {
                this.textBox_clientId.ReadOnly = true;
            }
            else
            {
                this.textBox_clientId.ReadOnly = false;
            }
        }
    }
}
