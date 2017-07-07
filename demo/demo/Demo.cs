/*
 * Licensed Materials - Property of Dasudian 
 * Copyright Dasudian Technology Co., Ltd. 2017 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using com.dasudian.iot.sdk;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication1
{
    class Demo
    {
        static DataHubClient client;

        static void Main(string[] args)
        {
            // 大数点IoT DataHub云端地址，
            // 请联系大数点商务support@dasudian.com获取
            string serverURL = "tcp://www.example.com:1883";
            // instance id, 标识客户的唯一ID，
            // 请联系大数点商务support@dasudian.com获取
            string instanceId = "yourInstanceId";
            // instance key, 与客户标识相对应的安全密钥，
            // 请联系大数点商务support@dasudian.com获取
            string instanceKey = "yourInstanceKey";
            // 客户端唯一ID，不同的客户端ID不能相同
            string clientId = "test_csharp_client";
            //客户端名字
            string userName = clientId;

            //打开追踪
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;

            //创建客户端实例
            client = new DataHubClient.Builder(instanceId,
                    instanceKey, userName, clientId)
                .SetServerURL(serverURL)
                .Build();
            //接收到消息的回调函数
            client.MessageReceived += client_MessageReceived;
            //连接状态改变的回调函数
            client.ConnectionStatusChanged += client_ConnectionStatusChanged;

            string topic = "mytopic";
            int ret;
            string content = "SendRequest content";

            //订阅主题, 最大以qos1的服务质量接收消息, 最大超时时间为10秒
            ret = client.Subscribe(topic, DataHubClient.QOS1, 10);
            Console.WriteLine("subscribe result:" + ret);

            //发布消息
            while (true)
            {
                Message message = new Message();
                //消息内容
                message.payload = Encoding.UTF8.GetBytes(content);
                //发布qos1消息, 超时时间为10s
                ret = client.SendRequest(topic, message,
                        DataHubClient.QOS1, 10);
                Console.WriteLine("SendRequest result:" + ret);
                Thread.Sleep(2000);
            }
            //客户端关闭,断开与服务器连接
            client.Destroy();
        }

        //连接状态改变的回调函数
        static void client_ConnectionStatusChanged(object sender,
                ConnectionStatusChangedEventArgs e)
        {
            Console.WriteLine("client_ConnectionStatusChanged:" +
                    e.IsConnected);
        }

        // 收到消息时的回调函数，可通过e.Message查看收到的具体消息内容
        private static void client_MessageReceived(object sender,
                MessageEventArgs e)
        {
            Console.WriteLine("Topic=" + e.Topic + ",Message=" +
                    Encoding.UTF8.GetString(e.Message));
        }
    }
}
