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
        /// 大数点IoT DataHub云端地址，
        /// 请联系大数点商务support@dasudian.com获取
        private string serverURL = "yourServerURL";
        /// instance id, 标识客户的唯一ID，
        /// 请联系大数点商务support@dasudian.com获取
        private string instanceId = "yourInstanceId";
        /// instance key, 与客户标识相对应的安全密钥，
        /// 请联系大数点商务support@dasudian.com获取
        private string instanceKey = "yourInstanceKey";

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
