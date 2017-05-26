using com.dasudian.iot.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatahubDemo
{
    class SubscribeDemo
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

        public static void Subscribe()
        {
            string userName = Guid.NewGuid().ToString();
            string clientId = userName;
            // create a DataHubClient client by DataHubClient.Builder
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, userName, clientId)
                .SetServerURL(serverURL).Build();

            // Asynchronous Subscribe a topic
            int messageId = client.Subscribe("test", DataHubClient.QOS_LEVEL_EXACTLY_ONCE);
            Console.WriteLine("messageId:" + messageId);

            // disconnect server
            client.Destroy();
        }
    }
}
