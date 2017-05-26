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
        private string serverURL = "tcp://try.iotdatahub.net:1883";// 大数点datahub公有云测试服务器地址
        private string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";// 大数点datahub公有云测试instanceId
        private string instanceKey = "238f173d6cc0608a";// 大数点datahub公有云测试instanceKey

        public SendRequestDemo()
        {
        }

        public void SendRequest()
        {
            string clientName = Guid.NewGuid().ToString();
            string clientId = clientName;
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, clientName, clientId)
                .SetServerURL(serverURL).Build();

            Message message = new Message();
            message.payload = Encoding.UTF8.GetBytes("hello world");
            int ret = client.SendRequest("test", message, DataHubClient.QOS_LEVEL_EXACTLY_ONCE, 5000);
            Console.WriteLine("ret = " + ret);
            if (ret == Constants.ERROR_NONE)
            {
                Console.WriteLine("SendRequest success");
            }
            else
            {
                Console.WriteLine("SendRequest failed,ret = " + ret);
            }

            client.Destroy();
        }
    }
}
