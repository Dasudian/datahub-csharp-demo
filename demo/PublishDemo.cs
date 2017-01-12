using com.dasudian.iot.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatahubDemo
{
    class PublishDemo
    {
        private static string serverURI = "try.iotdatahub.net";
        private static string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";
        private static string instanceKey = "238f173d6cc0608a";

        public static void Publish()
        {
            string userName = Guid.NewGuid().ToString();
            string clientId = userName;
            // create a DataHubClient client by DataHubClient.Builder
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, userName, clientId)
                .SetServerURI(serverURI).Build();
            // set a delegate to receive publish result
            client.Published += client_Published;
            // connect server
            int ret = client.Connect();
            // Asynchronous send message
            int messageId = client.Publish("test", Encoding.UTF8.GetBytes("hello world"), DataHubClient.QOS_LEVEL_EXACTLY_ONCE);
            Console.WriteLine("messageId = " + messageId);
            // disconnect server
            client.Disconnect();
        }

        /// <summary>
        /// a delegate to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_Published(object sender, PublishedEventArgs e)
        {
            Console.WriteLine("publish success,messageId = " + e.MessageId);
        }
    }
}
