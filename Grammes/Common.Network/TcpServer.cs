namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Sockets;

  using DataBaseAndNetwork.EventLog;

  using Messages;
  using Messages.MessageReceived;

  using Packets;

  public class TcpServer
  {
    #region Fields

    private readonly IPEndPoint _listenAddress;
    private readonly ConcurrentDictionary<IPEndPoint, TcpConnection> _connections;

    private SocketAsyncEventArgs _acceptEvent;
    private Socket _server;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Constructors

    public TcpServer(IPEndPoint listenAddress)
    {
      _listenAddress = listenAddress;
      _connections = new ConcurrentDictionary<IPEndPoint, TcpConnection>();
    }

    #endregion

    #region Methods

    public void Start()
    {
      _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      _server.Bind(_listenAddress);
      _acceptEvent = new SocketAsyncEventArgs();
      _acceptEvent.Completed += AcceptCompleted;
      _server.Listen(8);
      Accept();
    }

    public void Stop()
    {
      Socket server = _server;
      _server = null;
      server.Dispose();

      TcpConnection[] connections = _connections.Select(item => item.Value).ToArray();
      foreach (TcpConnection connection in connections)
      {
        connection.Stop();
      }

      _connections.Clear();
    }

    public void Send(string message)
    {
      byte[] messageBroadcast = new MessageBroadcast(message).GetBytes();

      foreach (KeyValuePair<IPEndPoint, TcpConnection> connection in _connections)
      {
        connection.Value.Send(messageBroadcast);
      }
    }

    internal void HandlePacket(IPEndPoint remoteEndpoint, byte[] packet)
    {
      if (!_connections.TryGetValue(remoteEndpoint, out TcpConnection connection))
      {
        return;
      }

      var packetId = (PacketId)BufferPrimitives.GetUint8(packet, 0);
      switch (packetId)
      {
        case PacketId.ConnectionRequest:
          var connectionRequest = new ConnectionRequest(packet);
          if (_connections.Values.Any(item => item.Login == connectionRequest.Login))
          {
            connection.Send(new ConnectionResponse(ResponseType.Failure, $"Клиент с именем '{connectionRequest.Login}' уже подключен.").GetBytes());
          }
          else
          {
            connection.Login = connectionRequest.Login;
            connection.Send(new ConnectionResponse(ResponseType.Ok, string.Empty).GetBytes());
            var eventLogMessage = new EventLogMessage
            {
              IsSuccessfully = true,
              SenderName = connection.Login,
              Text = "Connect completed",
              Time = DateTime.Now,
              Type = DispatchType.EventLog
            };
            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, true, eventLogMessage));
          }

          break;
        case PacketId.MessageRequest:
          var messageRequest = new MessageRequest(packet);
          MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Login, messageRequest.Message, new GeneralAgenda(), DateTime.Now));
          break;
      }
    }

    internal void FreeConnection(IPEndPoint remoteEndpoint)
    {
      if (_connections.TryRemove(remoteEndpoint, out TcpConnection connection) && !string.IsNullOrEmpty(connection.Login))
      {
        var eventLogMessage = new EventLogMessage
        {
          IsSuccessfully = true,
          SenderName = connection.Login,
          Text = "Disconnect completed",
          Time = DateTime.Now,
          Type = DispatchType.EventLog
        };
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, false, eventLogMessage));
      }
    }

    private void Accept()
    {
      _acceptEvent.AcceptSocket = null;
      if (_server?.AcceptAsync(_acceptEvent) == false)
      {
        AcceptCompleted(_server, _acceptEvent);
      }
    }

    private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
    {
      var remoteEndpoint = (IPEndPoint)e.AcceptSocket.RemoteEndPoint;
      var connection = new TcpConnection(remoteEndpoint, e.AcceptSocket, this);
      if (_connections.TryAdd(remoteEndpoint, connection))
      {
        connection.Start();
      }

      Accept();
    }

    #endregion
  }
}
