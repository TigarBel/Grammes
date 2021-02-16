namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;

  using Messages;
  using Messages.EventLog;

  using Newtonsoft.Json.Linq;

  using Properties;

  using WebSocketSharp.Server;

  public class WsServer
  {
    #region Fields

    private readonly IPEndPoint _listenAddress;
    private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

    private WebSocketServer _server;

    private readonly string _name;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Constructors

    public WsServer(IPEndPoint listenAddress)
    {
      _name = Resources.ServerName;
      _listenAddress = listenAddress;
      _connections = new ConcurrentDictionary<Guid, WsConnection>();
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

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      Container messageBroadcast = message.GetContainer();

      foreach (KeyValuePair<Guid, WsConnection> connection in _connections)
      {
        connection.Value.Send(messageBroadcast);
      }
    }

    internal void HandleMessage(Guid clientId, Container container)
    {
      if (!_connections.TryGetValue(clientId, out WsConnection connection))
      {
        return;
      }

      switch (container.Identifier)
      {
        case DispatchType.Login:
          if (((JObject)container.Payload).ToObject(typeof(LoginRequestContainer)) is LoginRequestContainer loginRequest)
          {
            var loginResponse = new LoginResponseContainer(DateTime.Now, new Response(ResponseStatus.Ok, "Connected"));

            if (_connections.Values.Any(item => item.Login == loginRequest.Content))
            {
              loginResponse.Content = new Response(ResponseStatus.Failure, $"Client with name '{loginRequest.Content}' yet connect.");
              connection.Login = $"pseudo-{loginRequest.Content}";
            }
            else
            {
              connection.Login = loginRequest.Content;
            }

            connection.Send(loginResponse.GetContainer());
            ConnectionStateChanged?.Invoke(
              this,
              new ConnectionStateChangedEventArgs(
                connection.Login,
                true,
                new EventLogMessage(_name, true, DispatchType.Login, loginResponse.Content.Reason, DateTime.Now)));
          }

          break;

        case DispatchType.Message:
          MessageReceived?.Invoke(this, MessageSorter.GetSortedMessage(connection.Login, (JObject)container.Payload));
          foreach (var connection in _connections)
          {
            
          }
          break;
      }
    }

    internal void AddConnection(WsConnection connection)
    {
      _connections.TryAdd(connection.Id, connection);
    }

    internal void FreeConnection(Guid connectionId)
    {
      if (_connections.TryRemove(connectionId, out WsConnection connection) && !string.IsNullOrEmpty(connection.Login))
      {
        ConnectionStateChanged?.Invoke(
          this,
          new ConnectionStateChangedEventArgs(
            connection.Login,
            false,
            new EventLogMessage(connection.Login, true, DispatchType.EventLog, "Disconnect", DateTime.Now)));
      }
    }

    #endregion
  }
}
