namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Threading;
  using System.Threading.Tasks;

  using Messages;
  using Messages.EventLog;

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

    public async void ConnectAsync(string address, int port)
    {
      if (IsConnected)
      {
        Disconnect();
      }

      _socket = new WebSocket($"ws://{address}:{port}");
      _socket.OnOpen += OnOpen;
      _socket.OnClose += OnClose;
      _socket.OnMessage += OnMessage;
      await Task.Run(_socket.ConnectAsync);
      Login();
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

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      _sendQueue.Enqueue(message.GetContainer());

      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
      {
        SendImpl();
      }
    }

    private void Login()
    {
      _sendQueue.Enqueue(new ConnectionRequestContainer(DateTime.Now, _login).GetContainer());

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
        var eventLog = new EventLogMessage(_login, false, DispatchType.MessageEventLog, "Doesn't completed", DateTime.Now);
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLog));
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
          var eventLog = new EventLogMessage(_login, true, DispatchType.ConnectionRequest, "Connect", DateTime.Now);
          if (((JObject)container.Payload).ToObject(typeof(ConnectionResponseContainer)) is ConnectionResponseContainer connectionResponse)
          {
            if (connectionResponse.Content.Result == ResponseStatus.Failure)
            {
              eventLog = new EventLogMessage(_login, false, DispatchType.ConnectionRequest, connectionResponse.Content.Reason, DateTime.Now);
              _login = string.Empty;
              MessageReceived?.Invoke(this, new MessageReceivedEventArgs(_login, connectionResponse.Content.Reason));
            }
          }

          ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, true, eventLog));
          break;

        case DispatchType.MessageRequest:
          if (((JObject)container.Payload).ToObject(typeof(MessageRequestContainer)) is MessageRequestContainer messageRequest)
          {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(_login, messageRequest.Content));
          }

          break;

        case DispatchType.MessageEventLog:
          break;
      }
    }

    private void OnClose(object sender, CloseEventArgs e)
    {
      var eventLog = new EventLogMessage(_login, true, DispatchType.MessageEventLog, "Close", DateTime.Now);
      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLog));
    }

    private void OnOpen(object sender, EventArgs e)
    {
      var eventLog = new EventLogMessage(_login, true, DispatchType.MessageEventLog, "Open", DateTime.Now);
      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, true, eventLog));
    }

    #endregion
  }
}
