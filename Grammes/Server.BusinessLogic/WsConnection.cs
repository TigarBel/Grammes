using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLogic
{
  using System.Collections.Concurrent;
  using System.Threading;

  using Common.Network.Messages;

  using Newtonsoft.Json;

  using WebSocketSharp;
  using WebSocketSharp.Server;

  public class WsConnection : WebSocketBehavior
  {
    #region Fields

    private readonly ConcurrentQueue<Container> _sendQueue;

    private WsServer _server;

    private int _sending;

    #endregion Fields

    #region Properties

    public Guid Id { get; }

    public string Login { get; set; }

    public bool IsConnected => Context.WebSocket?.ReadyState == WebSocketState.Open;

    #endregion Properties

    #region Constructors

    public WsConnection()
    {
      _sendQueue = new ConcurrentQueue<Container>();
      _sending = 0;

      Id = Guid.NewGuid();
    }

    #endregion Constructors

    #region Methods

    public void AddServer(WsServer server)
    {
      _server = server;
    }

    public void Send(Container container)
    {
      if (!IsConnected)
        return;

      _sendQueue.Enqueue(container);
      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
        SendImpl();
    }

    public void Close()
    {
      Context.WebSocket.Close();
    }

    protected override void OnOpen()
    {
      _server.AddConnection(this);
    }

    protected override void OnClose(CloseEventArgs e)
    {
      _server.FreeConnection(Id);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
      if (e.IsText) {
        var message = JsonConvert.DeserializeObject<Container>(e.Data);
        _server.HandleMessage(Id, message);
      }
    }

    private void SendCompleted(bool completed)
    {
      // При отправке произошла ошибка.
      if (!completed) {
        _server.FreeConnection(Id);
        Context.WebSocket.CloseAsync();
        return;
      }

      SendImpl();
    }

    private void SendImpl()
    {
      if (!IsConnected)
        return;

      if (!_sendQueue.TryDequeue(out var message) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
        return;

      var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
      string serializedMessages = JsonConvert.SerializeObject(message, settings);
      SendAsync(serializedMessages, SendCompleted);
    }

    #endregion Methods
  }
}
