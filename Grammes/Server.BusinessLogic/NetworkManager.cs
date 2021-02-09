namespace Server.BusinessLogic
{
  using System;
  using System.Net;

  using Common.Network;

  public class NetworkManager
  {
    #region Constants

    private const int WS_PORT = 65000;

    private const int TCP_PORT = 65001;

    #endregion

    #region Fields

    private readonly IPAddress _address = IPAddress.Parse("192.168.37.228");

    private readonly TcpServer _tcpServer;

    private readonly WsServer _wsServer;

    #endregion

    #region Constructors

    public NetworkManager()
    {
      _wsServer = new WsServer(new IPEndPoint(_address, WS_PORT));
      _wsServer.ConnectionStateChanged += HandleConnectionStateChanged;
      _wsServer.MessageReceived += HandleMessageReceived;

      _tcpServer = new TcpServer(new IPEndPoint(_address, TCP_PORT));
      _tcpServer.ConnectionStateChanged += HandleConnectionStateChanged;
      _tcpServer.MessageReceived += HandleMessageReceived;
    }

    #endregion

    #region Methods

    public void Start()
    {
      Console.WriteLine($"WebSocketServer: {_address}:{WS_PORT}");
      _wsServer.Start();

      Console.WriteLine($"TcpServer: {_address}:{TCP_PORT}");
      _tcpServer.Start();
    }

    public void Stop()
    {
      _wsServer.Stop();
      _tcpServer.Stop();
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      string clientState = e.Connected ? "подключен" : "отключен";
      string message = $"Клиент '{e.ClientName}' {clientState}.";

      Console.WriteLine(message);

      _wsServer.Send(message);
      _tcpServer.Send(message);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      string message = $"Клиент '{e.ClientName}' отправил сообщение '{e.Message}'.";

      Console.WriteLine(message);

      _wsServer.Send(message);
      _tcpServer.Send(message);
    }

    #endregion
  }
}
