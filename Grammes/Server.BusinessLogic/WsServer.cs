using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLogic
{
  using System.Collections.Concurrent;
  using System.Net;

  using Common.Network.Messages;

  using Newtonsoft.Json.Linq;

  using WebSocketSharp.Server;

  public class WsServer
  {
    #region Fields

    private readonly IPEndPoint _listenAddress;
    private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

    private WebSocketServer _server;

    #endregion Fields

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion Events

    #region Constructors

    public WsServer(IPEndPoint listenAddress)
    {
      _listenAddress = listenAddress;
      _connections = new ConcurrentDictionary<Guid, WsConnection>();
    }

    #endregion Constructors

    #region Methods

    public void Start()
    {
      _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
      //_server.AddWebSocketService("/", () => new WsConnection(this));
      _server.AddWebSocketService<WsConnection>("/",
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

      var connections = _connections.Select(item => item.Value).ToArray();
      foreach (var connection in connections) {
        connection.Close();
      }

      _connections.Clear();
    }

    public void Send(string message)
    {
      var messageBroadcast = new MessageBroadcastContainer(message).GetContainer();

      foreach (var connection in _connections) {
        connection.Value.Send(messageBroadcast);
      }
    }

    internal void HandleMessage(Guid clientId, Container container)
    {
      if (!_connections.TryGetValue(clientId, out WsConnection connection))
        return;

      switch (container.Identifier) {
        case DispatchType.ConnectionRequest:
          var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequestContainer)) as ConnectionRequestContainer;
          var connectionResponse = new ConnectionResponseContainer {Content = new Response(ResponseStatus.Ok,"")};
          if (_connections.Values.Any(item => item.Login == connectionRequest.Content)) {
            connectionResponse.Content = new Response(ResponseStatus.Failure, 
              $"Клиент с именем '{connectionRequest.Content}' уже подключен.");
            connection.Send(connectionResponse.GetContainer());
          } else {
            connection.Login = connectionRequest.Content;
            connection.Send(connectionResponse.GetContainer());
            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, true));
          }
          break;
        case DispatchType.MessageRequest:
          var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequestContainer)) as MessageRequestContainer;
          MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Login, messageRequest.Content));
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
        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, false));
    }

    #endregion Methods
  }
}
