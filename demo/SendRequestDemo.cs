using com.dasudian.iot.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatahubDemo
{
    /// <summary>
    /// This is a demo to show how to use datahub C# sdk
    /// </summary>
    class SendRequestDemo
    {
        /// <summary>
        /// 下面的服务器地址、instanceId和instanceKey为大数点公有云测试服务器地址。
        /// 注意：下面的服务器地址仅仅用于作为demo和简单测试使用，在正式使用大数点datahub服务时，
        /// 请联系大数点客服人员，获取私有的服务器地址、instanceId和instanceKey。
        /// </summary>
        private string serverURI = "try.iotdatahub.net";// 大数点datahub公有云测试服务器地址
        private string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";// 大数点datahub公有云测试instanceId
        private string instanceKey = "238f173d6cc0608a";// 大数点datahub公有云测试instanceKey

        public SendRequestDemo(string serverURI, string instanceId, string instanceKey)
        {
            this.serverURI = serverURI;
            this.instanceId = instanceId;
            this.instanceKey = instanceKey;
        }

        public void SendRequest()
        {
            // 客户端名字，可以填写任意的utf-8字符。
            // 如果你有第三账号系统，并想将自己的账号系统与大数点服务器同步，那么你可以使用第三方账号的名字、昵称。
            // 如果没有自己的账号系统，或者对该客户端名字不关心，可以使用随机的名字，但是不能填null。
            string clientName = Guid.NewGuid().ToString();
            // 客户端id，用于服务器唯一标记一个客户端，服务器通过该id向客户端推送消息;
            // 注意：不同的客户端的id不能相同，如果有两个相同的客户端id，服务器会关闭掉其中的一个客户端的连接。
            // 你可以使用设备的mac地址，或者第三方账号系统的id（比如qq号，微信号）。
            // 如果没有自己的账号系统，则可以随机生成一个不会重复的客户端id。
            // 或者自己指定客户端的id，只要能保证不同客户端id不同即可。
            string clientId = clientName;

            // 通过DataHubClient的Builder方法获取DataHubClient实例。
            // 在创建DataHubClient时，可以设置自己的配置参数，如果不设置，则使用SDK默认的设置。
            // 下面是一些配置选项的含义简单说明，如果想了解各个选项的详细含义，请查看API文档。
            // 配置1：SetAutomaticReconnect客户端是否自动重连服务器，默认为true，即客户端断开连接后，SDK会自动重连服务器。
            // 配置2：SetSecure是否加密传输，默认为fasle，表示不加密。
            // 配置3：SetServerURI设置服务器地址，默认为公有云测试服务器地址。
            // 配置4：SetCleanSession是否清除会话。true:即断开连接后，服务器是否保存该客户端（客户端通过客户端id来标记）订阅的topic。
            // false:断线后服务器保留客户端订阅的topic。默认为false。
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, clientName, clientId).SetServerURI(serverURI).Build();

            // 与大数点服务器建立一个长连接,连接成功返回0，连接失败返回错误码。
            // 注意：如果设置了自动重连，如果是由于网络异常（错误码0x06）导致的错误，
            // SDK依然会自动重连，其它错误SDK不会自动重连。当连接成功后，如果中途客户端连接断开了,
            // SDK自动重连的机制如下：等待2秒尝试连接服务器，如果连接失败，则等待4秒后，再次连接服务器，
            // 如果连接服务器失败，则在8秒后再尝试连接服务器,每次都将等待时间乘以2，直到最后最长等待64秒。
            int ret = client.Connect();
            if (ret == 0)
            {
                Console.WriteLine("connect success");
            }
            else
            {
                Console.WriteLine("connect failed,ret:" + ret);
            }

            // 发送消息到服务器，这个函数是同步方法。即会等待服务器返回或者超时才会返回。
            // 如果不想阻塞主线程，可以调用SDK的Publish发送消息，这个方法是异步发送消息的函数，不会阻塞主线程。
            // 超时时间通过SendRequest最后一个参数设置，单位为毫秒（ms）,当网络状况不佳的情况可以适当调大超时时间。
            ret = client.SendRequest("test", Encoding.UTF8.GetBytes("hello world"), DataHubClient.QOS_LEVEL_EXACTLY_ONCE, 5000);
            Console.WriteLine("ret = " + ret);
            if (ret == 0)
            {
                Console.WriteLine("SendRequest success");
            }
            else
            {
                Console.WriteLine("SendRequest failed,ret = " + ret);
            }

            // 断开与服务器的连接，如果客户端不想发送消息了，或者客户端不想接受服务器的消息了，
            // 则可以调用这个函数断开与服务器的长连接。
            client.Disconnect();
        }
    }
}
