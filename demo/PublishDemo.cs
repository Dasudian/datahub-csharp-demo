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
