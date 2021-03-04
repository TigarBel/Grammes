namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Net;
  using System.Net.Sockets;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;

  using DataBaseAndNetwork.EventLog;

  using Messages;
  using Messages.MessageSorter;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  using Packets;

  public class TcpClient
  {
    #region Constants

    private const int BUFFER_SIZE = ushort.MaxValue * 3;
    private const int SIZE_LENGTH = 2;

    private const int CONNECT_WAIT_TIME = 1100;

    #endregion

    #region Fields

    private readonly SocketAsyncEventArgs _receiveEvent;
    private readonly SocketAsyncEventArgs _sendEvent;
    private readonly SocketAsyncEventArgs _connectEvent;

    private readonly ConcurrentQueue<byte[]> _sendQueue;

    private readonly Socket _socket;

    private int _disposed;
    private int _sending;

    private string _login;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<LoginEventArgs> LoginEvent;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    public event EventHandler<UpdateChannelEventArgs> UpdateChannel;
    public event EventHandler<LogEventArgs> LogEvent;

    #endregion

    #region Constructors

    public TcpClient(string login)
    {
      _login = login;
      _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      _receiveEvent = new SocketAsyncEventArgs();
      _receiveEvent.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
      _receiveEvent.Completed += ReceiveCompleted;

      _sendEvent = new SocketAsyncEventArgs();
      _sendEvent.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
      _sendEvent.Completed += SendCompleted;

      _connectEvent = new SocketAsyncEventArgs();
      _connectEvent.Completed += ConnectCompleted;

      _sendQueue = new ConcurrentQueue<byte[]>();

      _disposed = 0;
      _sending = 0;
    }

    #endregion

    #region Methods

    public async void ConnectAsync(string address, int port)
    {
      if (_socket.Connected)
      {
        Disconnect();
      }

      _connectEvent.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
      _socket.ConnectAsync(_connectEvent);
      await Task.Run(CheckConnect);
    }

    public void Disconnect()
    {
      if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
      {
        return;
      }

      Safe(() => _socket.Dispose());
      Safe(() => _sendEvent.Dispose());
      Safe(() => _receiveEvent.Dispose());

      _login = string.Empty;
    }

    public void Login()
    {
      _sendQueue.Enqueue(ConvertToBytes(new LoginRequestContainer(_login).GetContainer()));

      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
      {
        SendImpl();
      }
    }

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      byte[] messageBroadcast = ConvertToBytes(message.GetContainer());
      _sendQueue.Enqueue(messageBroadcast);

      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
      {
        SendImpl();
      }
    }

    private byte[] ConvertToBytes(Container container)
    {
      var settings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore
      };
      string serializedMessages = JsonConvert.SerializeObject(container, settings);
      return new MessagePacket(serializedMessages).GetBytes();
    }

    private void CheckConnect()
    {
      Thread.Sleep(CONNECT_WAIT_TIME);
      if (!_socket.Connected)
      {
        var eventLog = new EventLogMessage
        {
          IsSuccessfully = false,
          SenderName = _login,
          Text = "No connection",
          Time = DateTime.Now,
          Type = DispatchType.Connection
        };
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLog));
      }
    }

    private void SendImpl()
    {
      if (_disposed == 1)
      {
        return;
      }

      if (!_sendQueue.TryDequeue(out byte[] packet) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
      {
        return;
      }

      // Установить размер в начале пакета.
      Array.Copy(packet, 0, _sendEvent.Buffer, SIZE_LENGTH, packet.Length);
      BufferPrimitives.SetUint16(_sendEvent.Buffer, 0, (ushort)packet.Length);

      _sendEvent.SetBuffer(0, packet.Length + SIZE_LENGTH);

      if (!_socket.SendAsync(_sendEvent))
      {
        SendCompleted(_socket, _sendEvent);
      }
    }

    private void SendCompleted(object sender, SocketAsyncEventArgs eventArgs)
    {
      if (eventArgs.BytesTransferred != eventArgs.Count || eventArgs.SocketError != SocketError.Success)
      {
        Disconnect();
        var eventLogMessage = new EventLogMessage
        {
          IsSuccessfully = false,
          SenderName = _login,
          Text = "Send don't completed",
          Time = DateTime.Now,
          Type = DispatchType.Message
        };
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLogMessage));
        return;
      }

      SendImpl();
    }

    private void Receive()
    {
      if (_disposed == 1)
      {
        return;
      }

      if (!_socket.ReceiveAsync(_receiveEvent))
      {
        ReceiveCompleted(_socket, _receiveEvent);
      }
    }

    private void ReceiveCompleted(object sender, SocketAsyncEventArgs eventArgs)
    {
      if (eventArgs.BytesTransferred == 0 || eventArgs.SocketError != SocketError.Success)
      {
        Disconnect();
        var eventLogMessage = new EventLogMessage
        {
          IsSuccessfully = false,
          SenderName = _login,
          Text = "Receive don't completed",
          Time = DateTime.Now,
          Type = DispatchType.Message
        };
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLogMessage));
        return;
      }

      int available = eventArgs.Offset + eventArgs.BytesTransferred;
      for (;;)
      {
        if (available < SIZE_LENGTH)
        {
          break;
        }

        int offset = 0;
        ushort length = BufferPrimitives.GetUint16(eventArgs.Buffer, ref offset);
        if (length + SIZE_LENGTH > available)
        {
          break;
        }

        HandlePacket(BufferPrimitives.GetBytes(eventArgs.Buffer, ref offset, length));

        available = available - length - SIZE_LENGTH;
        if (available > 0)
        {
          Array.Copy(eventArgs.Buffer, length + SIZE_LENGTH, eventArgs.Buffer, 0, available);
        }
      }

      if (!_socket.Connected) return;
      eventArgs.SetBuffer(available, BUFFER_SIZE - available);
      Receive();
    }

    private string GetStringPacket(byte[] packet)
    {
      int offset = 1;
      return Encoding.UTF8.GetString(packet, offset, packet.Length - offset);
    }

    private void HandlePacket(byte[] packet)
    {
      string serializedMessages = GetStringPacket(packet);
      var container = JsonConvert.DeserializeObject<Container>(serializedMessages);

      switch (container.Identifier)
      {
        case DispatchType.Login:
          if (((JObject)container.Payload).ToObject(typeof(LoginResponseContainer)) is LoginResponseContainer loginResponse)
          {
            var eventLog = new EventLogMessage
            {
              IsSuccessfully = true,
              SenderName = _login,
              Text = "Login",
              Time = DateTime.Now,
              Type = DispatchType.Login
            };
            if (loginResponse.Content.Result == ResponseType.Failure)
            {
              eventLog.IsSuccessfully = false;
              eventLog.Text = loginResponse.Content.Reason;
            }

            LoginEvent?.Invoke(
              this,
              new LoginEventArgs(
                _login,
                eventLog.IsSuccessfully,
                eventLog,
                loginResponse.General,
                loginResponse.OnlineList,
                loginResponse.OfflineList));
          }

          break;
        case DispatchType.Message:
          MessageReceived?.Invoke(this, MessageSorter.GetSortedMessage((JObject)container.Payload));
          break;
        case DispatchType.Channel:
          UpdateChannel?.Invoke(this, MessageSorter.GetSortedChannel((JObject)container.Payload));
          break;
        case DispatchType.EventLog:
          LogEvent?.Invoke(this, MessageSorter.GetSortedEventMessage((JObject)container.Payload));
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void ConnectCompleted(object sender, SocketAsyncEventArgs eventArgs)
    {
      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = true,
        SenderName = _login,
        Text = "Connect completed",
        Time = DateTime.Now,
        Type = DispatchType.EventLog
      };

      if (eventArgs.SocketError != SocketError.Success)
      {
        eventLogMessage.Text = "Connect don't completed";
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, false, eventLogMessage));
        return;
      }

      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(_login, true, eventLogMessage));
      Login();
      Receive();
    }

    private void Safe(Action callback)
    {
      try
      {
        callback();
      }
      catch
      {
        // ignored
      }
    }

    #endregion
  }
}
