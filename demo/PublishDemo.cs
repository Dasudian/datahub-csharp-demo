using com.dasudian.iot.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DatahubDemo
{
    class PublishDemo
    {
        /// 大数点IoT DataHub云端地址，
        /// 请联系大数点商务support@dasudian.com获取
        private string serverURL = "yourServerURL";
        /// instance id, 标识客户的唯一ID，
        /// 请联系大数点商务support@dasudian.com获取
        private string instanceId = "yourInstanceId";
        /// instance key, 与客户标识相对应的安全密钥，
        /// 请联系大数点商务support@dasudian.com获取
        private string instanceKey = "yourInstanceKey";

        public static void Publish()
        {
            string userName = Guid.NewGuid().ToString();
            string clientId = userName;
            // create a DataHubClient client by DataHubClient.Builder
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, userName, clientId)
                .SetServerURL(serverURL).Build();
            // Asynchronous send message
            Message message = new Message();
            message.payload = Encoding.UTF8.GetBytes("hello world");
            int messageId = client.Publish("test", message, DataHubClient.QOS_LEVEL_EXACTLY_ONCE);
            Console.WriteLine("messageId = " + messageId);
            Thread.Sleep(5000);
            // disconnect server
            client.Destroy();
        }
    }
}
