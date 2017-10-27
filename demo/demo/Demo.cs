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
            // 服务器地址，如果不设置该地址，则使用大数点公有云服务器
            string serverURL = "tcp://www.example.com:1883";
            // 从大数点客服务获取的instanceId
            string instanceId = "yourInstanceId";
            // 从大数点客服获取的instanceKey
            string instanceKey = "yourInstanceKey";
            // 客户端唯一ID，不同的客户端ID不能相同
            string clientId = "test_csharp_client";
            //客户端名字
            string clientType = "sensor";

            //打开追踪
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;

            //创建客户端实例
            client = new DataHubClient.Builder(instanceId, instanceKey, clientType, clientId)
                .SetServerURL(serverURL).SetDebug(true).SetCleanSession(true).Build();
            //接收到消息的回调函数
            client.MessageReceived += client_MessageReceived;
            //连接状态改变的回调函数
            client.ConnectionStatusChanged += client_ConnectionStatusChanged;

            string topic = "justTopic";
            int ret;
            string content = "only test";

            String content2 = "[\n" +
                  " {\n" +
                  " \"id\":\"01\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"0\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " },\n" +
                  " {\n" +
                  " \"id\":\"01\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"0\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " },\n" +
                  " {\n" +
                  " \"id\":\"011\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"01\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " },\n" +
                  " {\n" +
                  " \"id\":\"03\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"0\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " },\n" +
                  " {\n" +
                  " \"id\":\"04\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"0\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " },\n" +
                  " {\n" +
                  " \"id\":\"05\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"0\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " },\n" +
                  " {\n" +
                  " \"id\":\"06\",\n" +
                  " \"open\":false,\n" +
                  " \"pId\":\"0\",\n" +
                  " \"name\":\"A部门\"\n" +
                  " }\n" +
                  "]\n" +
                  "\n";

            do
            {
                //订阅主题 参数为: topic    Qos消息服务质量    超时时间(单位秒)     
                ret = client.Subscribe(topic, DataHubClient.QOS_LEVEL_EXACTLY_ONCE, 10);
                Console.WriteLine("subscribe result:" + ret);
                if (ret != 0)
                {
                    Thread.Sleep(2000);
                }
            } while (ret != 0);
            //订阅主题成功,才发布消息
            while (true)
            {
                Message message = new Message();
                //消息内容
                message.payload = Encoding.UTF8.GetBytes(content2);
                //发布消息 参数: topic  消息    Qos消息服务质量    超时时间(单位为秒)
                ret = client.SendRequest(topic, message, DataHubClient.QOS_LEVEL_EXACTLY_ONCE, 10, Constants.JSON);
                Console.WriteLine("SendRequest result:" + ret);
                Thread.Sleep(2000);
            }
            //客户端关闭,断开与服务器连接
            client.Destroy();

        }

        //连接状态改变的回调函数
        static void client_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs e)
        {
            Console.WriteLine("client_ConnectionStatusChanged:" + e.IsConnected);
        }

        /// <summary>
        /// 收到消息时的回调函数，可通过e.Message查看收到的具体消息内容
        /// </summary>
        private static void client_MessageReceived(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Topic=" + e.Topic + ",Message=" + Encoding.UTF8.GetString(e.Message));
        }
    }
}
