﻿namespace Common.Network
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;

  using DataBaseAndNetwork.EventLog;

  using Messages;
  using Messages.MessageReceived;
  using Messages.MessageSorter;

  using Newtonsoft.Json.Linq;

  using WebSocketSharp.Server;

  public class WsServer : BaseServer<Guid, WsConnection, WebSocketServer>, IServerable, ILaunchable
  {
    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Constructors

    /// <summary>
    /// Web Server
    /// </summary>
    /// <param name = "listenAddress">IP and Port</param>
    /// <param name = "timeout">Seconds</param>
    public WsServer(IPEndPoint listenAddress, int timeout)
      : base(listenAddress, timeout)
    {
    }

    #endregion

    #region Methods

    public void Start()
    {
      _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
      _server.AddWebSocketService<WsConnection>(
        "/",
        client =>
        {
          client.AddServer(this);
        });
      _server.Start();
    }

    public void Stop()
    {
      _server?.Stop();
      _server = null;

      WsConnection[] connections = _connections.Select(item => item.Value).ToArray();
      foreach (WsConnection connection in connections)
      {
        connection.Close();
      }

      _connections.Clear();
    }

    public void Send<TClass>(BaseContainer<TClass> message, BaseAgenda agenda)
    {
      Container messageRequest = message.GetContainer();
      switch (agenda.Type)
      {
        case ChannelType.General:
        {
          foreach (KeyValuePair<Guid, WsConnection> connection in _connections.Where(author => author.Value.Login != message.Author))
          {
            connection.Value.Send(messageRequest);
          }

          break;
        }
        case ChannelType.Private:
        {
          if (_connections.Values.Count == 0)
          {
            return;
          }

          _connections.Values.FirstOrDefault(item => item.Login == ((PrivateAgenda)agenda).Target)?.Send(messageRequest);
          break;
        }
      }
    }

    public bool IsContains(string user)
    {
      return UserOnlineList.Contains(user) || UserOfflineList.Contains(user);
    }

    internal void HandleMessage(Guid clientId, Container container)
    {
      if (!_connections.TryGetValue(clientId, out WsConnection connection))
      {
        return;
      }

      _connections.RefreshLifeClient(clientId);
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
              connection.Send(loginResponse.GetContainer());
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
          Send(MessageSorter.GetSortedMessage(message.Author, message.Message, InterfaceType.WebSocket, message.Agenda), message.Agenda);
          break;
      }
    }

    internal void AddConnection(WsConnection connection)
    {
      _connections.TryAdd(connection.Id, connection);
    }

    internal void FreeConnection(Guid connectionId)
    {
      if (!_connections.TryRemove(connectionId, out WsConnection connection) || string.IsNullOrEmpty(connection.Login))
      {
        return;
      }

      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = true,
        SenderName = connection.Login,
        Text = "Disconnect",
        Time = DateTime.Now,
        Type = DispatchType.EventLog
      };

      ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, false, eventLogMessage));
    }

    #endregion
  }
}
