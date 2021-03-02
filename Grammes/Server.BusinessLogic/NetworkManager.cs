namespace Server.BusinessLogic
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Threading.Tasks;

  using Common.DataBase;
  using Common.DataBase.DataBase.Table;
  using Common.DataBaseAndNetwork.EventLog;
  using Common.Network;
  using Common.Network.Collector;
  using Common.Network.Messages;
  using Common.Network.Messages.Channels;
  using Common.Network.Messages.MessageReceived;

  public class NetworkManager
  {
    #region Fields

    private readonly WsServer _wsServer;

    private readonly DataBaseManager _dataBaseManager;

    #endregion

    #region Constructors

    /// <summary>
    /// Manager of server
    /// </summary>
    /// <param name = "address">IP and Port</param>
    /// <param name = "timeout">Seconds life of client</param>
    public NetworkManager(IPEndPoint address, int timeout)
    {
      _dataBaseManager = new DataBaseManager();

      _wsServer = new WsServer(address, timeout);
      foreach (User user in _dataBaseManager.UserList)
      {
        _wsServer.UserOfflineList.Add(user.Name);
      }

      _wsServer.ConnectionStateChanged += HandleConnectionStateChanged;
      _wsServer.MessageReceived += HandleMessageReceived;
    }

    #endregion

    #region Methods

    public void Start()
    {
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
        _dataBaseManager.CreateUserAsync(
          new User
          {
            Name = eventArgs.ClientName
          });
      }

      if (eventArgs.EventLog.Type == DispatchType.Login && eventArgs.EventLog.IsSuccessfully)
      {
        SendLoginInitAsync(eventArgs);
      }

      if (eventArgs.EventLog.IsSuccessfully)
      {
        _wsServer.Send(new ChannelResponseContainer(new UpdateChannel(eventArgs.Connected, client, isRegistration), client), new GeneralAgenda());
      }

      string clientState = eventArgs.Connected ? "connect" : "disconnect";
      string message = $"Client '{client}' {clientState}.";
      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = eventArgs.EventLog.IsSuccessfully,
        SenderName = client,
        Text = message,
        Time = DateTime.Now,
        Type = DispatchType.Connection
      };
      EventActionAsync(new MessageEventLogContainer(eventLogMessage), new GeneralAgenda());
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
      string messageServer = $"{eventArgs.Agenda.Type}:{eventArgs.Author}:{eventArgs.Message}";
      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = true,
        SenderName = eventArgs.Author,
        Text = messageServer,
        Time = DateTime.Now,
        Type = DispatchType.Message
      };

      switch (eventArgs.Agenda.Type)
      {
        case ChannelType.General:
          User user = _dataBaseManager.UserList.Find(u => u.Name == eventArgs.Author);
          var generalMessage = new GeneralMessage
          {
            Message = eventArgs.Message,
            Time = eventArgs.Time,
            User_Id = user.Id
          };
          _dataBaseManager.CreateGeneralMessageAsync(generalMessage);
          EventActionAsync(new MessageEventLogContainer(eventLogMessage), eventArgs.Agenda);
          break;
        case ChannelType.Private:
          var privateMessage = new PrivateMessage
          {
            Message = eventArgs.Message,
            Time = eventArgs.Time,
            SenderId = _dataBaseManager.UserList.Find(u => u.Name == eventArgs.Author).Id,
            TargetId = _dataBaseManager.UserList.Find(u => u.Name == ((PrivateAgenda)eventArgs.Agenda).Target).Id
          };
          _dataBaseManager.CreatePrivateMessageAsync(privateMessage);
          EventActionAsync(new MessageEventLogContainer(eventLogMessage), eventArgs.Agenda);
          break;
        case ChannelType.Group:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private async void EventActionAsync(MessageEventLogContainer messageEvent, BaseAgenda agenda)
    {
      await Task.Delay(1000);
      await Task.Run(
        () =>
        {
          _wsServer.Send(messageEvent, agenda);
          _dataBaseManager.CreateEventAsync(
            new Event
            {
              IsSuccessfully = true,
              Message = messageEvent.Content.Text,
              Time = DateTime.Now,
              UserName = messageEvent.Content.SenderName,
              Type = DispatchType.Connection
            });
        });
    }

    private async void SendLoginInitAsync(ConnectionStateChangedEventArgs eventArgs)
    {
      await Task.Delay(1000);
      List<GeneralMessage> generalMessage = await _dataBaseManager.GetGeneralMessageAsync();
      List<PrivateMessage> privateMessages = await _dataBaseManager.GetPrivateMessageAsync();
      await Task.Run(
        () =>
        {
          User user = _dataBaseManager.UserList.Find(u => u.Name == eventArgs.ClientName);
          _wsServer.Send(
            new LoginResponseContainer(
              new Response(ResponseStatus.Ok, eventArgs.EventLog.Text),
              Collector.CollectGeneralChannel(user.Id, generalMessage),
              Collector.CollectOnlineChannel(user, _wsServer.UserOnlineList, privateMessages),
              Collector.CollectOfflineChannel(user, _wsServer.UserOfflineList, privateMessages)),
            new PrivateAgenda(eventArgs.ClientName));
        });
    }

    #endregion
  }
}
