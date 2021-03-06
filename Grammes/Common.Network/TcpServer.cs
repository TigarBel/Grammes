﻿namespace Common.Network
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Sockets;
  using System.Text;

  using DataBaseAndNetwork.EventLog;

  using Messages;
  using Messages.MessageReceived;
  using Messages.MessageSorter;

  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  using Packets;

  public class TcpServer : BaseServer<IPEndPoint, TcpConnection, Socket>, IServerable, ILaunchable
  {
    #region Fields

    private SocketAsyncEventArgs _acceptEvent;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Constructors

    /// <summary>
    /// Tcp Server
    /// </summary>
    /// <param name = "listenAddress">IP and Port</param>
    /// <param name = "timeout">Seconds</param>
    public TcpServer(IPEndPoint listenAddress, int timeout)
      : base(listenAddress, timeout)
    {
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
      if (_server == null)
      {
        return;
      }

      Socket server = _server;
      _server = null;
      server.Dispose();

      TcpConnection[] connections = _connections.Select(item => item.Value).ToArray();
      foreach (TcpConnection connection in connections)
      {
        connection.Close();
      }

      _connections.Clear();
    }

    public void Send<TClass>(BaseContainer<TClass> message, BaseAgenda agenda)
    {
      byte[] messageBroadcast = ConvertToBytes(message.GetContainer());

      switch (agenda.Type)
      {
        case ChannelType.General:
        {
          foreach (KeyValuePair<IPEndPoint, TcpConnection> connection in _connections.Where(author => author.Value.Login != message.Author))
          {
            connection.Value.Send(messageBroadcast);
          }

          break;
        }
        case ChannelType.Private:
        {
          if (_connections.Values.Count == 0)
          {
            return;
          }

          _connections.Values.FirstOrDefault(item => item.Login == ((PrivateAgenda)agenda).Target)?.Send(messageBroadcast);
          break;
        }
        case ChannelType.Group:
          throw new ArgumentOutOfRangeException();
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public bool IsContains(string user)
    {
      return UserOnlineList.Contains(user) || UserOfflineList.Contains(user);
    }

    internal void HandlePacket(IPEndPoint remoteEndpoint, byte[] packet)
    {
      string serializedMessages = GetStringPacket(packet);
      var container = JsonConvert.DeserializeObject<Container>(serializedMessages);

      if (!_connections.TryGetValue(remoteEndpoint, out TcpConnection connection))
      {
        return;
      }

      _connections.RefreshLifeClient(remoteEndpoint);
      switch (container.Identifier)
      {
        case DispatchType.Login:
          if (((JObject)container.Payload).ToObject(typeof(LoginRequestContainer)) is LoginRequestContainer loginRequest)
          {
            LoginResponseContainer loginResponse;
            bool isEnter = true;
            DispatchType stage;

            if (UserOnlineList.Contains(loginRequest.Content))
            {
              loginResponse = new LoginResponseContainer(
                new Response(ResponseType.Failure, $"Client with name '{loginRequest.Content}' yet connect."),
                null,
                null,
                null,
                null);
              connection.Send(ConvertToBytes(loginResponse.GetContainer()));
              connection.Login = $"pseudo-{loginRequest.Content}";
              stage = DispatchType.Connection;
            }
            else
            {
              isEnter = UserOfflineList.Contains(loginRequest.Content);
              loginResponse = new LoginResponseContainer(new Response(ResponseType.Ok, "Connected"), null, null, null, null);
              connection.Login = loginRequest.Content;
              stage = DispatchType.Login;
            }

            var eventLogMessage = new EventLogMessage
            {
              IsSuccessfully = loginResponse.Content.Result == ResponseType.Ok == isEnter,
              SenderName = _name,
              Text = loginResponse.Content.Reason,
              Time = DateTime.Now,
              Type = stage
            };

            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, true, eventLogMessage));
          }

          break;

        case DispatchType.Message:
          MessageReceivedEventArgs message = MessageSorter.GetSortedMessage((JObject)container.Payload);
          MessageReceived?.Invoke(this, message);
          Send(MessageSorter.GetSortedMessage(message.Author, message.Message, InterfaceType.Tcp, message.Agenda), message.Agenda);
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

    private byte[] ConvertToBytes(Container container)
    {
      var settings = new JsonSerializerSettings
      {
        NullValueHandling = NullValueHandling.Ignore
      };
      string serializedMessages = JsonConvert.SerializeObject(container, settings);
      return new MessagePacket(serializedMessages).GetBytes();
    }

    private string GetStringPacket(byte[] packet)
    {
      int offset = 1;
      return Encoding.UTF8.GetString(packet, offset, packet.Length - offset);
    }

    private void Accept()
    {
      _acceptEvent.AcceptSocket = null;
      if (_server?.AcceptAsync(_acceptEvent) == false)
      {
        AcceptCompleted(_server, _acceptEvent);
      }
    }

    private void AcceptCompleted(object sender, SocketAsyncEventArgs eventArgs)
    {
      var remoteEndpoint = (IPEndPoint)eventArgs.AcceptSocket.RemoteEndPoint;
      var connection = new TcpConnection(remoteEndpoint, eventArgs.AcceptSocket, this);
      if (_connections.TryAdd(remoteEndpoint, connection))
      {
        connection.Open();
      }

      Accept();
    }

    #endregion
  }
}
