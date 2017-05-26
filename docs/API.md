# Dasudian IoT DataHub C# SDK

1. [版本信息](#version)
2. [集成前准备](#ready)
3. [创建](#create)
4. [设置回调函数](#callback)
5. [订阅](#subscribe)
6. [取消订阅](#unsubscribe)
7. [异步发布](#publish)
8. [同步发布](#sendRequest)
9. [销毁](#destroy)
10. [错误码](#errorCode)
11. [Message](#Message)
12. [ConnectionStatusChangedEventArgs](#ConnectionStatusChangedEventArgs)
13. [MessageEventArgs](#MessageEventArgs)
14. [ServiceException](#ServiceException)
15. [QoS](#QoS)

## <a name="version">版本信息</a>

| Date | Version | Note |
|---|---|---|
| 2017.3.31 | 2.1.0 | 修改了异步发送消息函数，添加了异步发送结果回调函数 |
| 2017.3.27 | 2.0.0 | 客户端全面升级，版本更新为2.0.0 |


## <a name="ready">集成前准备</a>

- 在自己的项目中引入大数点IoT DataHub的动态库。将dasudian-datahub-sdk.x.x.x.dll和M2Mqtt.dll拷贝到你工程中exe文件的运行目录下。然后在你的项目中引用该dll库。

- 联系大数点客服人员获取instanceId和instanceKey。instanceId和instanceKey用于sdk和大数点服务器建立安全的通信。


## <a name="create">创建</a>

通过DataHubClient.Builder创建DataHubClient实例。

```
public class Builder
{
    ...
    /// <summary>
    /// 通过该Builer获取DataHubClient实例
    /// </summary>
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
    /// 注意：不同的客户端的id不能相同，如果有两个相同的客户端id，服务器会关闭掉其中的一个客户端的连接。
    /// 你可以使用设备的mac地址，或者第三方账号系统的id（比如qq号，微信号）。
    /// 如果没有自己的账号系统，则可以随机生成一个不会重复的客户端id。</param>
    public Builder(string instanceId, string instanceKey, string clientName, string clientId)
    {
        ...
    }

    /// <summary>
    /// 设置私有云服务器地址。如果不设置，则默认使用大数点公有云测试服务器。
    /// </summary>
    /// <param name="serverURL">服务器的地址，加密连接方式tcp://host:port，非加密连接方式ssl://host:port；
    /// 其中ssl和tcp都必须为小写，host可以使用域名或ip地址，port表示对应的端口。</param>
    /// <returns>返回当前Builder</returns>
    public Builder SetServerURL(string serverURL)
    {
    }

    /// <summary>
    /// 是否打开调试模式
    /// </summary>
    /// <param name="debug">true:打开调试模式；false:关闭调试。默认为false</param>
    /// <returns></returns>
    public Builder SetDebug(bool debug)
    {
    }

    /// <summary>
    /// 构建DataHubClient实例
    /// </summary>
    /// <returns>返回DataHubClient实例</returns>
    public DataHubClient Build()
    {
    }
}
```

## <a name="callback">设置回调函数(可选)</a>

设置回调函数接收服务器的消息，或者监听SDK与服务器的连接状态。

```
// 接收到消息的回调函数
public event MessageEventHandler MessageReceived;
// SDK与服务器连接状态改变的回调函数
public event ConnectionStatusChangedEventHandler ConnectionStatusChanged;
```

## <a name="subscribe">订阅</a>

该方法是同步方法，会阻塞主线程。

```
/// <summary>
/// 订阅
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="timeout">超时时间，单位s，必须大于0。表示该函数最多阻塞多长时间。对于普通的文本消息，建议超时时间为10s。
/// 注意：该函数超时返回不代表消息发送失败，仅表示在指定时间内没有接收到服务器的应答。</param>
/// <returns>成功：返回Constants.ERROR_NONE；失败：返回错误码</returns>
public int Subscribe(string topic, long timeout)
```

## <a name="unsubscribe">取消订阅</a>

该方法是同步方法，会阻塞主线程。

```
/// <summary>
/// 取消订阅
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="timeout">超时时间，单位s，必须大于0。表示该函数最多阻塞多长时间。对于普通的文本消息，建议超时时间为10s。
/// 注意：该函数超时返回不代表消息发送失败，仅表示在指定时间内没有接收到服务器的应答。</param>
/// <returns>成功：返回Constants.ERROR_NONE；失败：返回错误码</returns>
public int Unsubscribe(string topic, long timeout)
```

## <a name="publish">异步发布</a>

该方法是异步方法，不会阻塞主线程。

```
/// <summary>
/// 异步发布消息
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="message">消息内容；长度必须小于512k。</param>
/// <param name="qos">服务质量</param>
/// <param name="messageId">返回消息ID，可以通过在MessageDelivered回调函数中通过对比ID来确定该条消息的发送结果</param>
/// <returns>成功：消息成功递交给SDK，SDK会根据设置的qos等级来发送消息，但是不提供发送成功或失败的结果的回调函数。
/// 失败：消息递交给SDK失败</returns>
public int Publish(string topic, Message message, byte qos, out int messageId)
```

## <a name="sendRequest">同步发布</a>

该方法是同步方法，会阻塞主线程。

```
/// <summary>
/// 同步发布消息
/// </summary>
/// <param name="topic">主题名</param>
/// <param name="message">消息内容;长度必须小于512k。</param>
/// <param name="qos">服务质量</param>
/// <param name="timeout">超时时间，单位s，必须大于0。表示该函数最多阻塞多长时间。对于普通的文本消息，建议超时时间为10s。
/// 注意：该函数超时返回不代表消息发送失败，仅表示在指定时间内没有接收到服务器的应答。</param>
/// <returns>成功：返回Constants.ERROR_NONE；失败：返回错误码</returns>
public int SendRequest(string topic, Message message, byte qos, long timeout)
```

## <a name="destroy">销毁</a>

销毁客户端实例，并断开与服务器的连接。当客户端关闭时，调用这个函数。

```
/// <summary>
/// 销毁当前客户端，并断开与服务器的连接
/// </summary>
public void Destroy()
```

## <a name="errorCode">错误码</a>

当函数调用发生错误时，请查看下面的错误码，了解发生了什么错误。

```
public class Constants
{
    ...
    // 通用错误码
    public const int ERROR_NONE = 0;// 没有错误
    public const int ERROR_ILLEGAL_PARAMETERS = -1;// 非法参数
    public const int ERROR_DISCONNECTED = -2;// 没有与服务器连接
    public const int ERROR_UNACCEPT_PROTOCOL_VERSION = -3;// 协议版本不支持
    public const int ERROR_IDENTIFIER_REJECTED = -4;// 标识符已拒绝
    public const int ERROR_SERVER_UNAVAILABLE = -5;// 服务器不可用
    public const int ERROR_BAD_USERNAME_OR_PASSWD = -6;// 错误的用户名和密码
    public const int ERROR_UNAUTHORIZED = -7;// 未认证
    public const int ERROR_AUTHORIZED_SERVER_UNAVAILABLE = -8;// 认证服务器不可用
    public const int ERROR_OPERATION_FAILURE = -9;// 操作失败
    public const int ERROR_MESSAGE_TOO_BIG = -10;// 消息太大
    public const int ERROR_NETWORK_UNREACHABLE = -11;// 网络不可达
    public const int ERROR_TIMEOUT = -12;// 超时
    // C#特有错误码
}
```

## <a name="Message">Message</a>

发送消息使用的消息类型定义如下：

```
public class Message
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public byte[] payload { get; set; }
}
```

## <a name="ConnectionStatusChangedEventArgs">ConnectionStatusChangedEventArgs</a>

连接状态改变时，回调函数中参数的定义如下：

```
public class ConnectionStatusChangedEventArgs　:　EventArgs
{
    /// <summary>
    /// 当前连接状态。true:连接正常；false:与服务器连接断开。
    /// </summary>
    public bool IsConnected
    {
    }

    public ConnectionStatusChangedEventArgs(bool isConnected)
    {
    }
}
```

## <a name="MessageEventArgs">MessageEventArgs</a>

接收到消息时，回调函数中参数的定义如下：

```
public class MessageEventArgs : EventArgs
{
    #region Properties...

    /// <summary>
    /// 消息的主题
    /// </summary>
    public string Topic
    {
    }

    /// <summary>
    /// 消息的内容
    /// </summary>
    public byte[] Message
    {
    }

    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="topic">消息的主题</param>
    /// <param name="message">消息的内容</param>
    public MessageEventArgs(string topic, byte[] message)
    {
    }
}
```

## <a name="ServiceException">ServiceException</a>

创建实例时，如果填写了不正确的参数，则会抛出该异常。

```
public class ServiceException : Exception
{
    public ServiceException(DataHubClientErrorCode errorCode)
    {
    }


    /// <summary>
    /// 获取错误码
    /// </summary>
    public DataHubClientErrorCode ErrorCode
    {
    }
}

public enum DataHubClientErrorCode
{
    EXCEPTION_ILLEGAL_PARAMETERS = 1 // 非法参数
}
```

## <a name="QoS">QoS</a>

服务质量。QoS越大，服务质量越高，消耗的时间和网络流量也越大。

```
0:最多分发一次；仅仅发送出去，不等待服务器的应答。
1:至少分发一次，可能会重复发送；发送给服务器，并等待服务器的应答。如果在一段时间没有接收到服务器ack，客户端会重新发送该条消息。
2:保证消息只会被分发一次。
```