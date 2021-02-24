namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;

  using DataBase;

  using Messages;
  using Messages.EventLog;
  using Messages.MessageReceived;
  using Messages.MessageSorter;

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
    
    public List<string> UserOnlineList { get; set; }

    public List<string> UserOfflineList { get; set; }

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

      UserOfflineList = new List<string>();
      UserOnlineList = new List<string>();
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
          foreach (KeyValuePair<Guid, WsConnection> connection in _connections.Where(
            author => author.Value.Login != message.Author)
          )
          {
            connection.Value.Send(messageRequest);
          }

          break;
        }
        case ChannelType.Private:
        {
          _connections.Values.First(item => item.Login == ((PrivateAgenda)agenda).Target).Send(messageRequest);
          break;
        }
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
            LoginResponseContainer loginResponse;
            bool isEnter = true;
            DispatchType stage;

            if (_connections.Values.Any(item => item.Login == loginRequest.Content))
            {
              loginResponse = new LoginResponseContainer(
                new Response(ResponseStatus.Failure, $"Client with name '{loginRequest.Content}' yet connect."),
                null,
                null);
              connection.Login = $"pseudo-{loginRequest.Content}";
              stage = DispatchType.Connection;
            }
            else
            {
              UserOnlineList.Add(loginRequest.Content);
              UserOnlineList.Sort();
              isEnter = UserOfflineList.Remove(loginRequest.Content);
              loginResponse = new LoginResponseContainer(new Response(ResponseStatus.Ok, "Connected"), 
                UserOnlineList, UserOfflineList);
              connection.Login = loginRequest.Content;
              stage = DispatchType.Login;
            }

            connection.Send(loginResponse.GetContainer());
            ConnectionStateChanged?.Invoke(
              this,
              new ConnectionStateChangedEventArgs(
                connection.Login,
                true,
                new EventLogMessage(_name, loginResponse.Content.Result == ResponseStatus.Ok == isEnter,
                  stage, loginResponse.Content.Reason, DateTime.Now)));
          }

          break;

        case DispatchType.Message:
          MessageReceivedEventArgs message = MessageSorter.GetSortedEventMessage((JObject)container.Payload);
          MessageReceived?.Invoke(this, message);
          Send(MessageSorter.GetSortedMessage(message.Author, message.Message, message.Agenda), message.Agenda);
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

      UserOfflineList.Add(connection.Login);
      UserOfflineList.Sort();
      bool isExit = UserOnlineList.Remove(connection.Login);
      ConnectionStateChanged?.Invoke(
        this,
        new ConnectionStateChangedEventArgs(
          connection.Login,
          false,
          new EventLogMessage(connection.Login, isExit, DispatchType.EventLog, "Disconnect", DateTime.Now)));
    }

    #endregion
  }
}
