namespace Server.BusinessLogic
{
  using System;
  using System.Linq;
  using System.Net;

  using Common.DataBase;
  using Common.DataBase.DataBase;
  using Common.Network;
  using Common.Network.Messages;
  using Common.Network.Messages.Channels;
  using Common.Network.Messages.MessageReceived;

  public class NetworkManager
  {
    #region Fields

    private readonly IPEndPoint _address;

    private readonly WsServer _wsServer;

    private readonly UsersList _usersList;

    #endregion

    #region Constructors

    public NetworkManager()
    {
      _address = new Config.ServerConfig().GetAddress();
      _usersList = new UsersList();
      _wsServer = new WsServer(_address, new BaseManager());

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

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs eventArgs)
    {
      if (_usersList.GetUsersList().All(item => item != eventArgs.ClientName) && eventArgs.Connected)
      {
        _wsServer.Send(
          new LoginResponseContainer(new Response(ResponseStatus.Failure, "Not in the database"), null, null),
          new PrivateAgenda(eventArgs.ClientName));
      }

      string clientState = eventArgs.Connected ? "connect" : "disconnect";
      string message = $"Client '{eventArgs.ClientName}' {clientState}.";
      _wsServer.Send(
        new ChannelResponseContainer(new UpdateChannel(eventArgs.Connected, eventArgs.ClientName)), 
        new GeneralAgenda());
      Console.WriteLine(message);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
      string message = $"Client '{eventArgs.Author}' send message '{eventArgs.Message}'.";

      Console.WriteLine(message);
    }

    #endregion
  }
}
