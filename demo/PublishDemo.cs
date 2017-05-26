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
        private static string serverURL = "tcp://try.iotdatahub.net:1883";
        private static string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";
        private static string instanceKey = "238f173d6cc0608a";

        public static void Publish()
        {
            string userName = Guid.NewGuid().ToString();
            string clientId = userName;
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, userName, clientId)
                .SetServerURL(serverURL).Build();

            client.MessageDelivered += client_MessageDelivered;

            Message message = new Message();
            message.payload = Encoding.UTF8.GetBytes("hello world");
            int messageId;
            int ret = client.Publish("test", message, DataHubClient.QOS_LEVEL_EXACTLY_ONCE, out messageId);
            Console.WriteLine("messageId = " + messageId);
            Thread.Sleep(5000);
            // disconnect server
            client.Destroy();
        }

        /// <summary>
        /// 异步发送结果回调函数
        /// </summary>
        /// <param name="messageId">消息ID,在调用publish时通过参数返回的消息id</param>
        /// <param name="result">成功：true；失败：false</param>
        static void client_MessageDelivered(int messageId, bool result)
        {
            Console.WriteLine("messageId=" + messageId + ", result=" + result);
        }
    }
}
