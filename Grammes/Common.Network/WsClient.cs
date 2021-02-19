﻿namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Threading;
  using System.Threading.Tasks;

  using Messages;
  using Messages.EventLog;
  using Messages.MessageSorter;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  using WebSocketSharp;

  public class WsClient
  {
    #region Constants

    private const int CONNECT_WAIT_TIME = 2000;

    #endregion

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
    public event EventHandler<LoginEventArgs> LoginEvent;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    public event EventHandler<UpdateChannelEventArgs> UpdateChannel;

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
      _socket.ConnectAsync();
      await Task.Run(CheckConnect);
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
        Close();
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

    private void CheckConnect()
    {
      Thread.Sleep(CONNECT_WAIT_TIME);
      if (!IsConnected)
      {
        var eventLog = new EventLogMessage(_login, false, DispatchType.Connection, "No connection", DateTime.Now);
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLog));
      }
    }

    private void Login()
    {
      _sendQueue.Enqueue(new LoginRequestContainer(_login).GetContainer());

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
        var eventLog = new EventLogMessage(_login, false, DispatchType.Message, "Doesn't completed", DateTime.Now);
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
        case DispatchType.Login:
          if (((JObject)container.Payload).ToObject(typeof(LoginResponseContainer)) is LoginResponseContainer loginResponse)
          {
            var eventLog = new EventLogMessage(_login, true, DispatchType.Login, "Login", DateTime.Now);
            if (loginResponse.Content.Result == ResponseStatus.Failure)
            {
              eventLog.IsSuccessfully = false;
              eventLog.Text = loginResponse.Content.Reason;
            }

            LoginEvent?.Invoke(this, new LoginEventArgs(_login, eventLog.IsSuccessfully, eventLog, 
              loginResponse.OnlineList, loginResponse.OfflineList));
          }

          break;
        case DispatchType.Message:
          MessageReceived?.Invoke(this, MessageSorter.GetSortedEventMessage((JObject)container.Payload));
          break;
        case DispatchType.Channel:
          UpdateChannel?.Invoke(this, MessageSorter.GetSortedChannel((JObject)container.Payload));
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void OnClose(object sender, CloseEventArgs e)
    {
      Close();
    }

    private void Close()
    {
      var eventLog = new EventLogMessage(_login, true, DispatchType.Connection, "Close", DateTime.Now);
      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLog));
    }

    private void OnOpen(object sender, EventArgs e)
    {
      var eventLog = new EventLogMessage(_login, true, DispatchType.Connection, "Open", DateTime.Now);
      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, true, eventLog));
      Login();
    }

    #endregion
  }
}
