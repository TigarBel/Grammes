namespace Server.BusinessLogic
{
  using System;
  using System.Linq;
  using System.Net;

  using Common.DataBase;
  using Common.DataBase.DataBase;
  using Common.DataBase.DataBase.Table;
  using Common.Network;
  using Common.Network.Messages;
  using Common.Network.Messages.Channels;
  using Common.Network.Messages.MessageReceived;

  public class NetworkManager
  {
    #region Fields

    private readonly IPEndPoint _address;

    private readonly WsServer _wsServer;

    private readonly DataBaseManager _dataBaseManager;

    #endregion

    #region Constructors

    public NetworkManager()
    {
      _address = new Config.ServerConfig().GetAddress();
      _dataBaseManager = new DataBaseManager();
      _wsServer = new WsServer(_address, _dataBaseManager);

      using (SqlUserRepository db = new SqlUserRepository()) {
        // создаем два объекта User
        User user1 = new User { Name = "Tom" };
        User user2 = new User { Name = "Sam" };

        // добавляем их в бд
        db.Create(user1);
        db.Create(user2);
        db.Save();
        Console.WriteLine("Объекты успешно сохранены");

        // получаем объекты из бд и выводим на консоль
        var users = db.GetItemList();
        Console.WriteLine("Список объектов:");
        foreach (User u in users) {
          Console.WriteLine($"{u.Id}.{u.Name}");
        }
      }

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
      string client = eventArgs.ClientName;
      bool isRegistration = false;
      if (eventArgs.EventLog.Type == DispatchType.Login && !eventArgs.EventLog.IsSuccessfully)
      {
        eventArgs.EventLog.IsSuccessfully = true;
        isRegistration = true;
      }

      if (eventArgs.EventLog.IsSuccessfully)
      {
        _wsServer.Send(new ChannelResponseContainer(new UpdateChannel(eventArgs.Connected, client, isRegistration), client), new GeneralAgenda());
      }

      string clientState = eventArgs.Connected ? "connect" : "disconnect";
      string message = $"Client '{client}' {clientState}.";

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
