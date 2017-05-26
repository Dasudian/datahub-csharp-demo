using com.dasudian.iot.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatahubDemo
{
    class SubscribeDemo
    {
        private static string serverURL = "tcp://try.iotdatahub.net:1883";
        private static string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";
        private static string instanceKey = "238f173d6cc0608a";

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
