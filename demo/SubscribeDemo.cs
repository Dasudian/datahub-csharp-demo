using com.dasudian.iot.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatahubDemo
{
    class SubscribeDemo
    {
        private static string serverURI = "try.iotdatahub.net";
        private static string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";
        private static string instanceKey = "238f173d6cc0608a";

        public static void Subscribe()
        {
            string userName = Guid.NewGuid().ToString();
            string clientId = userName;
            // create a DataHubClient client by DataHubClient.Builder
            DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, userName, clientId)
                .SetServerURI(serverURI).Build();

            // set a delegate to receive subscribe result
            client.Subscribed += client_Subscribed;

            // connect server
            int ret = client.Connect();

            // Asynchronous Subscribe a topic
            int messageId = client.Subscribe("test", DataHubClient.QOS_LEVEL_EXACTLY_ONCE);
            Console.WriteLine("messageId:" + messageId);

            // disconnect server
            client.Disconnect();
        }

        /// <summary>
        /// 这个代理方法在Subscribe成功后会调用。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_Subscribed(object sender, SubscribedEventArgs e)
        {
            Console.WriteLine("subscribe success,messageId:" + e.MessageId);
        } 
    }
}
