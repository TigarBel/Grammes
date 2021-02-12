namespace Server.BusinessLogic
{
  using System;
  using System.Collections.Concurrent;
  using System.Threading;

  using Common.Network.Messages;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  using WebSocketSharp;

  public class WsClient
  {
    #region Fields

    private readonly ConcurrentQueue<Container> _sendQueue;

    private WebSocket _socket;

    private int _sending;
    private string _login;

    #endregion

    #region Properties

    public bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Constructors

    public WsClient(string login)
    {
      _login = login;
      _sendQueue = new ConcurrentQueue<Container>();
      _sending = 0;
    }

    #endregion

    #region Methods

    public void Connect(string address, int port)
    {
      if (IsConnected)
      {
        Disconnect();
      }

      _socket = new WebSocket($"ws://{address}:{port}");
      _socket.OnOpen += OnOpen;
      _socket.OnClose += OnClose;
      _socket.OnMessage += OnMessage;
      _socket.ConnectAsync();
    }

    public void Disconnect()
    {
      if (_socket == null)
      {
        return;
      }

      if (IsConnected)
      {
        _socket.CloseAsync();
      }

      _socket.OnOpen -= OnOpen;
      _socket.OnClose -= OnClose;
      _socket.OnMessage -= OnMessage;

      _socket = null;
      _login = string.Empty;
    }

    public void Send(string message)
    {
      _sendQueue.Enqueue(new MessageRequestContainer(message).GetContainer());

      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
      {
        SendImpl();
      }
    }

    private void Login()
    {
      _sendQueue.Enqueue(new ConnectionRequestContainer(_login).GetContainer());

      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
      {
        SendImpl();
      }
    }

    private void SendCompleted(bool completed)
    {
      // При отправке произошла ошибка.
      if (!completed)
      {
        Disconnect();
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false));
        return;
      }

      SendImpl();
    }

    private void SendImpl()
    {
      if (!IsConnected)
      {
        return;
      }

      if (!_sendQueue.TryDequeue(out Container message) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
      {
        return;
      }

      var settings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore
      };
      string serializedMessages = JsonConvert.SerializeObject(message, settings);
      _socket.SendAsync(serializedMessages, SendCompleted);
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
      if (!e.IsText)
      {
        return;
      }

      var container = JsonConvert.DeserializeObject<Container>(e.Data);

      switch (container.Identifier)
      {
        case DispatchType.ConnectionResponse:
          var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponseContainer)) as ConnectionResponseContainer;
          if (connectionResponse.Content.Result == ResponseStatus.Failure)
          {
            _login = string.Empty;
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(_login, connectionResponse.Content.Reason));
          }

          ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, true));
          break;
        case DispatchType.MessageBroadcast:
          var messageBroadcast = ((JObject)container.Payload).ToObject(typeof(MessageBroadcastContainer)) as MessageBroadcastContainer;
          MessageReceived?.Invoke(this, new MessageReceivedEventArgs(_login, messageBroadcast.Content));
          break;
      }
    }

    private void OnClose(object sender, CloseEventArgs e)
    {
      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false));
    }

    private void OnOpen(object sender, EventArgs e)
    {
      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, true));
      Login();
    }

    #endregion
  }
}
