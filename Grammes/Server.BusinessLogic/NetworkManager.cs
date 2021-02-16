namespace Server.BusinessLogic
{
  using System;
  using System.Net;

  using Common.Network;
  using Common.Network.Messages;

  public class NetworkManager
  {
    #region Fields

    private readonly IPEndPoint _address;

    private readonly WsServer _wsServer;

    #endregion

    #region Constructors

    public NetworkManager()
    {
      _address = new Config.ServerConfig().GetAddress();
      _wsServer = new WsServer(_address);
      _wsServer.ConnectionStateChanged += HandleConnectionStateChanged;
      _wsServer.MessageReceived += HandleMessageReceived;
    }

    #endregion

    #region Methods

    public void Start()
    {
      Console.WriteLine($"WebSocketServer: {_address.Address}:{_address.Port}");
      _wsServer.Start();
    }

    public void Stop()
    {
      _wsServer.Stop();
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      string clientState = e.Connected ? "connect" : "disconnect";
      var response = new Response(ResponseStatus.Ok, clientState);

      string message = $"Client '{e.ClientName}' {clientState}.";
      Console.WriteLine(message);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      string message = $"Client '{e.ClientName}' send message '{e.Message}'.";

      Console.WriteLine(message);
    }

    #endregion
  }
}
