namespace Server.BusinessLogic
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
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
  using Common.Network.Messages.MessageSorter;

  public class NetworkManager
  {
    #region Fields

    private readonly WsServer _wsServer;
    private readonly TcpServer _tcpServer;

    private readonly DataBaseManager _dataBaseManager;

    #endregion

    #region Constructors

    /// <summary>
    /// Manager of server
    /// </summary>
    /// <param name = "address">IP and Port</param>
    /// <param name = "timeout">Seconds life of client</param>
    /// <param name = "dataBaseManager">Base manager</param>
    public NetworkManager(IPEndPoint address, int timeout, DataBaseManager dataBaseManager)
    {
      _dataBaseManager = dataBaseManager;

      _wsServer = new WsServer(address, timeout);
      _tcpServer = new TcpServer(new IPEndPoint(address.Address, address.Port + 1), timeout);
      foreach (User user in _dataBaseManager.UserList)
      {
        _wsServer.UserOfflineList.Add(user.Name);
        _tcpServer.UserOfflineList.Add(user.Name);
      }

      _wsServer.ConnectionStateChanged += HandleConnectionStateChanged;
      _wsServer.MessageReceived += HandleMessageReceived;

      _tcpServer.ConnectionStateChanged += HandleConnectionStateChanged;
      _tcpServer.MessageReceived += HandleMessageReceived;
    }

    #endregion

    #region Methods

    public void Start()
    {
      _wsServer.Start();
      _tcpServer.Start();
    }

    public void Stop()
    {
      _wsServer.Stop();
      _tcpServer.Stop();
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
        AddNewUserOnList(client);
      }

      if (eventArgs.EventLog.Type == DispatchType.Login && eventArgs.EventLog.IsSuccessfully)
      {
        SendLoginInitAsync(eventArgs);
      }

      if (eventArgs.EventLog.IsSuccessfully && Contains(client))
      {
        var crc = new ChannelResponseContainer(new UpdateChannel(eventArgs.Connected, client, isRegistration), client);
        _wsServer.Send(crc, new GeneralAgenda());
        _tcpServer.Send(crc, new GeneralAgenda());
      }

      string clientState = eventArgs.Connected ? "connect" : "disconnect";
      string message = $"Client '{client}' {clientState}.";
      AddUserOnList(eventArgs.Connected, client);

      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = eventArgs.EventLog.IsSuccessfully,
        SenderName = client,
        Text = message,
        Time = DateTime.Now,
        Type = DispatchType.Connection
      };
      EventSaveAsync(new MessageEventLogContainer(eventLogMessage), new GeneralAgenda());
    }

    private bool Contains(string client)
    {
      return _wsServer.Contains(client) || _tcpServer.Contains(client);
    }

    private void AddUserOnList(bool connection, string client)
    {
      if (connection)
      {
        RearrangeUsers(_wsServer.UserOfflineList, _wsServer.UserOnlineList, client);
        RearrangeUsers(_tcpServer.UserOfflineList, _tcpServer.UserOnlineList, client);
      }
      else
      {
        RearrangeUsers(_wsServer.UserOnlineList, _wsServer.UserOfflineList, client);
        RearrangeUsers(_tcpServer.UserOnlineList, _tcpServer.UserOfflineList, client);
      }
    }

    private void RearrangeUsers(List<string> fromList, List<string> inList, string user)
    {
      if (fromList.Remove(user))
      {
        inList.Add(user);
        inList.Sort();
      }
    }

    private void AddNewUserOnList(string client)
    {
      _wsServer.UserOnlineList.Add(client);
      _wsServer.UserOnlineList.Sort();
      _tcpServer.UserOnlineList.Add(client);
      _tcpServer.UserOnlineList.Sort();
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
          SendCurrentServer(eventArgs.Type, MessageSorter.GetSortedMessage(eventArgs), eventArgs.Agenda);
          User user = _dataBaseManager.UserList.Find(u => u.Name == eventArgs.Author);
          var generalMessage = new GeneralMessage
          {
            Message = eventArgs.Message,
            Time = eventArgs.Time,
            User_Id = user.Id
          };
          _dataBaseManager.CreateGeneralMessageAsync(generalMessage);
          EventSaveAsync(new MessageEventLogContainer(eventLogMessage), eventArgs.Agenda);
          break;
        case ChannelType.Private:
          SendCurrentServer(eventArgs.Type, MessageSorter.GetSortedMessage(eventArgs), eventArgs.Agenda);
          var privateMessage = new PrivateMessage
          {
            Message = eventArgs.Message,
            Time = eventArgs.Time,
            SenderId = _dataBaseManager.UserList.Find(u => u.Name == eventArgs.Author).Id,
            TargetId = _dataBaseManager.UserList.Find(u => u.Name == ((PrivateAgenda)eventArgs.Agenda).Target).Id
          };
          _dataBaseManager.CreatePrivateMessageAsync(privateMessage);
          EventSaveAsync(new MessageEventLogContainer(eventLogMessage), eventArgs.Agenda);
          break;
        case ChannelType.Group:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private async void EventSaveAsync(MessageEventLogContainer messageEvent, BaseAgenda agenda)
    {
      await Task.Delay(1000);
      await Task.Run(
        () =>
        {
          _wsServer.Send(messageEvent, agenda);
          _tcpServer.Send(messageEvent, agenda);
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
          var lrc = new LoginResponseContainer(
            new Response(ResponseType.Ok, eventArgs.EventLog.Text),
            Collector.CollectGeneralChannel(user.Id, generalMessage),
            Collector.CollectOnlineChannel(user, CheckUserList(_wsServer.UserOnlineList, _tcpServer.UserOnlineList), privateMessages),
            Collector.CollectOfflineChannel(user, CheckUserList(_wsServer.UserOfflineList, _tcpServer.UserOfflineList), privateMessages));
          _wsServer.Send(lrc, new PrivateAgenda(eventArgs.ClientName));
          _tcpServer.Send(lrc, new PrivateAgenda(eventArgs.ClientName));
        });
    }

    private List<string> CheckUserList(List<string> firstList, List<string> secondList)
    {
      if (firstList.All(secondList.Contains))
      {
        return new List<string>(firstList);
      }

      throw new ArgumentException("Out of sync in user lists on servers");
    }

    private void SendCurrentServer<TContainer>(InterfaceType type, BaseContainer<TContainer> message, BaseAgenda agenda)
    {
      switch (type)
      {
        case InterfaceType.WebSocket:
          _tcpServer.Send(message, agenda);
          break;
        case InterfaceType.TcpSocket:
          _wsServer.Send(message, agenda);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
    }

    #endregion
  }
}
