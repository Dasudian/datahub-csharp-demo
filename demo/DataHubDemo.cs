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
        private int subMessageId;
        private int unsubMessageId;
        private int pubMessageId;
        // 用于在接收消息时刷新UI的代理函数
        private delegate void MessageDelegate(string message);

        private void MessageReceived(string message)
        {
            this.messageReceive.Text = message;
        }

        private void button_connect(object sender, EventArgs e)
        {
            // 下面的服务器地址、instanceId和instanceKey为大数点公有云测试服务器地址。
            // 注意：下面的服务器地址仅仅用于作为demo和简单测试使用，在正式使用大数点datahub服务时，
            // 请联系大数点客服人员(www.dasudian.com)，获取私有的服务器地址、instanceId和instanceKey。
            string serverURI = this.serverURI.Text;
            string instanceId = this.instanceId.Text;
            string instanceKey = this.instanceKey.Text;
            // 客户端名字，可以填写任意的utf-8字符。
            // 如果你有第三账号系统，并想将自己的账号系统与大数点服务器同步，那么你可以使用第三方账号的名字、昵称。
            // 如果没有自己的账号系统，或者对该客户端名字不关心，可以使用随机的名字，但是不能填null。
            string clientName = null;
            if (this.checkBox_random_clientname.Checked)
            {
                clientName = Guid.NewGuid().ToString();
            }
            else
            {
                clientName = this.textBox_userName.Text;
            }
             

            // 客户端id，用于服务器唯一标记一个客户端，服务器通过该id向客户端推送消息;
            // 注意：不同的客户端的id不能相同，如果有两个相同的客户端id，服务器会关闭掉其中的一个客户端的连接。
            // 你可以使用设备的mac地址，或者第三方账号系统的id（比如qq号，微信号）。
            // 如果没有自己的账号系统，则可以随机生成一个不会重复的客户端id。
            // 或者自己指定客户端的id，只要能保证不同客户端id不同即可。
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

            // 通过DataHubClient的Builder方法获取DataHubClient实例。
            // 在创建DataHubClient时，可以设置自己的配置参数，如果不设置，则使用SDK默认的设置。
            // 下面是一些配置选项的含义简单说明，如果想了解各个选项的详细含义，请查看API文档。
            // 配置1：SetAutomaticReconnect客户端是否自动重连服务器，默认为true，即客户端断开连接后，SDK会自动重连服务器。
            // 配置2：SetSecure是否加密传输，默认为fasle，表示不加密。
            // 配置3：SetServerURI设置服务器地址，默认为公有云测试服务器地址。
            // 配置4：SetCleanSession是否清除会话。即断开连接后，服务器是否保存该客户端（客户端通过客户端id来标记）订阅的topic。
            bool cleansession = this.clean_session.Checked;
            bool secure = this.checkBox_Secure.Checked;
            bool autoReconnect = this.auto_reconnect.Checked;
            client = new DataHubClient.Builder(instanceId, instanceKey, clientName, clientId)
                .SetAutomaticReconnect(autoReconnect).SetCleanSession(cleansession).SetSecure(secure)
                .SetServerURI(serverURI).Build();

            // 与大数点服务器建立一个长连接,连接成功返回0，连接失败返回错误码。
            // 注意：如果设置了自动重连，如果是由于网络异常（错误码0x06）导致的错误，
            // SDK依然会自动重连，其它错误SDK不会自动重连。当连接成功后，如果中途客户端连接断开了,
            // SDK自动重连的机制如下：等待2秒尝试连接服务器，如果连接失败，则等待4秒后，再次连接服务器，
            // 如果连接服务器失败，则在8秒后再尝试连接服务器,每次都将等待时间乘以2，直到最后最长等待64秒。
            int ret = client.Connect();
            if (ret == 0)
            {
                this.connectResult.Image = PICTURE_OK;
            }
            else
            {
                this.connectResult.Image = PICTURE_NG;
                MessageBox.Show("connect error:" + ret);
            }
        }

        /// <summary>
        /// 订阅一个topic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_subscribe(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected())
            {
                MessageBox.Show("请先连接服务器");
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
            client.Subscribed += client_Subscribed;
            client.MessageReceived += client_MessageReceived;
            this.subMessageId = client.Subscribe(topic, qos);
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

        /// <summary>
        /// 订阅结果的代理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_Subscribed(object sender, SubscribedEventArgs e)
        {
            if (subMessageId == e.MessageId)
            {
                this.subResult.Image = PICTURE_OK;
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.Disconnect();
            }
        }

        private void unsubscribe_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected())
            {
                MessageBox.Show("请先连接服务器");
                return;
            }
            string topic = this.text_sub_or_unsub_qos.Text;
            if (topic == null)
            {
                MessageBox.Show("topic不能为null");
                return;
            }
            client.Unsubscribed += client_Unsubscribed;
            this.unsubMessageId = client.Unsubscribe(topic);
        }

        void client_Unsubscribed(object sender, UnsubscribedEventArgs e)
        {
            if (unsubMessageId == e.MessageId)
            {
                this.unsub_result.Image = PICTURE_OK;
            }
        }

        private void publish_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected())
            {
                MessageBox.Show("请先连接服务器");
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
            client.Published += client_Published;
            pubMessageId = client.Publish(topic, Encoding.UTF8.GetBytes(payload), qos);
        }

        void client_Published(object sender, PublishedEventArgs e)
        {
            if (pubMessageId == e.MessageId)
            {
                this.picture_pub_result.Image = PICTURE_OK;
            }
        }

        /// <summary>
        /// 同步发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendrequest_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected())
            {
                MessageBox.Show("请先连接服务器");
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
            int ret = client.SendRequest(topic, Encoding.UTF8.GetBytes(payload), qos, 10000);
            if (ret == 0)
            {
                this.picture_sendquest_result.Image = PICTURE_OK;
            }
            else
            {
                this.picture_sendquest_result.Image = PICTURE_NG;
                MessageBox.Show("connect error:" + ret);
            }
        }

        /// <summary>
        /// 每隔1秒获取一次sdk的连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (client != null)
            {
                if (client.IsConnected())
                {
                    this.picture_connect_status.Image = PICTURE_OK;
                }
                else
                {
                    this.picture_connect_status.Image = PICTURE_NG;
                }
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
