## General Info
These are the C# APIs which use protocol MQTT to tranfer massively real-time data to Dasudian IoT Cloud

Most APIs are asynchronous.

You can publish the infomation collecting from a device to the cloud. Also you can subscribe to
a topic, receive a message and handle the message.

To use the SDK:

1.  Create a client object

2.  Set or not set the options for connecting to a cloud instance

3.  Subsrcibe to topics depending on whether the client needs to receive messages

4.  Repeatedly publishing messages or handling messages

5.  Disconnect the client object


## 集成前准备

- 在自己的项目中引入大数点IoT DataHub的动态库。将DataHub.dll和M2Mqtt.dll拷贝到你工程中exe文件的运行目录下。然后在你的项目中引用该dll库。

- 到大数点官网，联系大数点客服人员获取instanceId和instanceKey。instanceId和instanceKey用于sdk和大数点服务器建立安全的通信。


## 创建DataHubClient实例

使用DataHubClient的Builder创建一个DataHubClient实例。

```
/// <summary>
/// 注意：下面的服务器地址仅仅用于作为demo和简单测试使用，在正式使用大数点datahub服务时，
/// 请联系大数点客服人员，获取私有的服务器地址、instanceId和instanceKey。
/// </summary>

private string serverURI = "try.iotdatahub.net";// 大数点datahub公有云测试服务器地址
private string instanceId = "dsd_9FmYSNiqpFmi69Bui0_A";// 大数点datahub公有云测试instanceId
private string instanceKey = "238f173d6cc0608a";// 大数点datahub公有云测试instanceKey

// 客户端名字，可以填写任意的utf-8字符。
// 如果你有第三方账号系统，并想将自己的账号系统与大数点服务器同步，那么你可以使用第三方账号的名字、昵称。
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
// 配置2：SetSecure是否加密传输，默认为true，表示加密。
// 配置3：SetServerURI设置私有云服务器地址。默认为公有云测试服务器地址。
// 配置4：SetCleanSession是否清除会话。true:即断开连接后，服务器是否保存该客户端（客户端通过客户端id来标记）订阅的topic。
// false:断线后服务器保留客户端订阅的topic。默认为false。
DataHubClient client = new DataHubClient.Builder(instanceId, instanceKey, clientName, clientId).SetServerURI(serverURI).Build();
```

在创建客户端时的Builder详细说明如下：

```
public class Builder
{
    /// <param name="instanceId">用于大数点验证用户，保证客户端与服务器间的安全通信。
    /// demo中的instanceId仅可以用于测试大数点IoT DataHub功能使用，
    /// 如果您想正式使用大数点IoT服务，请联系大数点客服获取私有的instanceId</param>
    /// <param name="instanceKey">用于大数点验证用户，保证客户端与服务器间的安全通信。
    /// demo中的instanceKey仅可以用于测试大数点IoT DataHub功能使用，
    /// 如果您想正式使用大数点IoT服务，请联系大数点客服获取私有的instanceKey</param>
    /// <param name="clientName">客户端名字，可以填写任意的utf-8字符。
    /// 如果你有第三账号系统，并想将自己的账号系统与大数点服务器同步，那么你可以使用第三方账号的名字、昵称。
    /// 如果没有自己的账号系统，或者对该客户端名字不关心，可以使用随机的名字，但是不能填null。</param>
    /// <param name="clientId">客户端id，用于服务器唯一标记一个客户端，服务器通过该id向客户端推送消息;
    /// 注意：不同的客户端的id必须不同，如果有两个客户端有相同的id，服务器会关掉其中的一个客户端的连接。
    /// 可以使用设备的mac地址，或者第三方账号系统的id（比如qq号，微信号）。
    /// 如果没有自己的账号系统，则可以随机生成一个不会重复的客户端id。
    /// 或者自己指定客户端的id，只要能保证不同客户端id不同即可。</param>
    public Builder(string instanceId, string instanceKey, string clientName, string clientId);

    /// <summary>
    /// 构建DataHubClient实例
    /// </summary>
    /// <returns>返回DataHubClient实例</returns>
    public DataHubClient Build();

    /// <summary>
    /// 设置是否开启sdk内部的自动重连功能
    /// </summary>
    /// <param name="automaticReconnect">true:打开sdk内部自动重连功能，
    /// false：关闭sdk内部自动重连功能。默认为true。
    /// SDK自动重连的机制如下：等待2秒尝试连接服务器，如果连接失败，则等待4秒后，再次连接服务器，
    /// 如果连接服务器失败，则在8秒后再尝试连接服务器,每次都将等待时间乘以2，直到最后最长等待64秒。</param>
    /// <returns>返回当前Builder</returns>
    public DataHubClient.Builder SetAutomaticReconnect(bool automaticReconnect);

    /// <summary>
    /// 设置是否清除会话
    /// </summary>
    /// <param name="cleanSession">true:清除会话，在客户端断开连接后，订阅的topic不会在服务器保存。
    /// false：不清除会话，断开连接后订阅的topic会保留。默认为false</param>
    /// <returns>返回当前Builder</returns>
    public DataHubClient.Builder SetCleanSession(bool cleanSession);

    /// <summary>
    /// 设置是否加密
    /// </summary>
    /// <param name="secure">true：使用加密的方式传输。false：使用不加密的方式传输</param>
    /// <returns>返回当前Builder</returns>
    public DataHubClient.Builder SetSecure(bool secure);

    /// <summary>
    /// 设置私有云服务器地址
    /// </summary>
    /// <param name="serverURI">服务器地址，如果不设置，则默认使用大数点公有云测试服务器。</param>
    /// <returns>返回当前Builder</returns>
    public DataHubClient.Builder SetServerURI(string serverURI);
}
```

## 设置回调函数(可选)

```
// 接收服务器或者其他客户端发布的消息
public event MessageEventHandler MessageReceived;
// 监听Publish函数的的结果
public event PublishedEventHandler Published;
// 监听Subscribe结果
public event SubscribedEventHandler Subscribed;
// 监听Unsubscribe结果
public event UnsubscribedEventHandler Unsubscribed;
// 监听SDK与服务连接断开。连接成功后，如果SDK中途断开，则会回调这个函数。
public event ConnectionLostEventHandler ConnectionLost;
// 连接错误回调函数。在第一调用connect函数时，由于网络原因，没有得到服务器的返回。
// 此时SDK会自动重连，如果在网络可用时，服务器返回连接错误代码，则SDK会调用此函数，并停止自动重连。
public event ConnectErrorEventHandler ConnectError;
```

## 连接服务器

```
/// <summary>
/// 连接大数点IoT DataHub服务器
/// 注意：所有的后续函数调用都必须在连接服务器成功后才能继续进行。
/// </summary>
/// <returns>
/// 成功：0
/// 失败：
/// 0x01连接已拒绝，不支持的协议版本，sdk返回错误码，不开启自动重连。
/// 0x02连接已拒绝，不合格的客户端标识符，sdk返回错误码，不开启自动重连。
/// 0x03连接已拒绝，服务端不可用，sdk返回错误码，是否继续重连?
/// 0x04连接已拒绝，无效的用户名或密码，sdk返回错误码，不开启自动重连。
/// 0x05连接已拒绝，未授权，sdk返回错误码，不开启自动重连。
/// 0x06网络异常,SDK会自动重连。
public byte Connect()
```

## 订阅

该方法为异步方法，不会阻塞主线程。

```
/// <summary>
/// 订阅
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="qos">服务质量</param>
/// <returns>成功：返回消息id，该id可以用来在Subscribed回调函数中用来确认消息是否发送成功；
/// 失败：返回错误码，在文档最后查看错误码。</returns>
public int Subscribe(string topic, byte qos)
```

服务质量说明

```
// qos(quality of service),服务质量
// 最多发送一次，发送失败了不会再发送
public const byte QOS_LEVEL_AT_MOST_ONCE = 0x00;
// 至少发送一次，可能会发送多次
public const byte QOS_LEVEL_AT_LEAST_ONCE = 0x01;
// 发送失败后会重发，但是会确保服务器只收到一次
public const byte QOS_LEVEL_EXACTLY_ONCE = 0x02;
```

## 取消订阅

```
/// <summary>
/// 取消订阅
/// </summary>
/// <param name="topic">主题名</param>
/// <returns>成功：返回消息id，可以在Unsubscribed中确定消息是否发送成功；
/// 失败：返回错误码，在文档最后查看错误码。</returns>
public int Unsubscribe(string topic)
```

## 发布消息

异步发送

```
/// <summary>
/// 发送异步消息
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="message">消息内容;最大支持消息长度为512k，超过该长度的消息将不能发送到服务器。</param>
/// <param name="qos">服务质量</param>
/// <returns>成功：返回消息id，可在Published回调函数中确定消息是否发送成功；
/// 失败：返回错误码，在文档最后查看错误码。</returns>
public int Publish(string topic, byte[] message, byte qos)
```

同步发送

```
/// <summary>
/// 发送同步请求
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="message">消息内容;最大支持消息长度为512k，超过该长度的消息将不能发送到服务器。</param>
/// <param name="qos">服务质量</param>
/// <param name="timeout">超时时间，多长时间没有收到服务的应答视为超时，单位ms</param>
/// <returns>返回结果码：成功：Constants.SUCCESS；失败：Constants.ERROR_SERVER_NOT_CONNECTED(服务器未连接),
/// Constants.ERROR_SNED_TIMEOUT(发送超时)</returns>
public int SendRequest(string topic, byte[] message, byte qos, Int32 timeout)
```

## 获取sdk连接状态

```
/// <summary>
/// 获取当前sdk与服务器的连接状态
/// </summary>
/// <returns>true：sdk与服务器连接正常；false：sdk与服务器连接断开</returns>
public bool IsConnected()
```

## 断开与服务器的连接

```
/// <summary>
/// 断开与服务器的连接
/// </summary>
public void Disconnect()
```

## 错误码

```
public class Constants
{
    ......
    // error code
    public const int SUCCESS = 0;// 成功
    public const int ERROR_SERVER_NOT_CONNECTED = -1000;// 服务器未连接
    public const int ERROR_SNED_TIMEOUT = -1001; // 消息发送超时
    public const int ERROR_ILLEGAL_PARAMETERS = -1003; // 非法的参数
    public const byte ERROR_NETWORK_EXCEPTION = 0x06; // 连接时网络异常
}
```