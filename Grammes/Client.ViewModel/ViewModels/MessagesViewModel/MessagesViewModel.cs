namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;
  using System.Linq;

  using BusinessLogic.Model.Network;

  using Common.DataBaseAndNetwork.EventLog;
  using Common.Network.ChannelsListModel;
  using Common.Network.Messages;
  using Common.Network.Messages.MessageReceived;

  using EventAggregator;

  using Extension;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  public class MessagesViewModel : BindableBase
  {
    #region Fields

    private string _loginName;

    private BaseChannel _channel;

    private AsyncObservableCollection<MessageViewModel> _messagesUserList;

    private DelegateCommand _commandSendMessage;

    private string _textMessage;

    private bool _isAvailable;

    private readonly ICurrentConnection _currentConnection;

    #endregion

    #region Properties

    public BaseChannel Channel
    {
      get => _channel;
      set
      {
        SetProperty(ref _channel, value);
        MessagesUserList.Clear();
        foreach (MessageModel message in value.MessageList)
        {
          MessagesUserList.Add(new MessageViewModel(message));
        }

        Alert.AutomaticAlert(_loginName, _currentConnection);
      }
    }

    public AsyncObservableCollection<MessageViewModel> MessagesUserList
    {
      get => _messagesUserList;
      set => SetProperty(ref _messagesUserList, value);
    }

    public DelegateCommand CommandSendMessage
    {
      get => _commandSendMessage;
      set => SetProperty(ref _commandSendMessage, value);
    }

    public string TextMessage
    {
      get => _textMessage;
      set => SetProperty(ref _textMessage, value, Validate);
    }

    public bool IsAvailable
    {
      get => _isAvailable;
      set => SetProperty(ref _isAvailable, value);
    }

    #endregion

    #region Constructors

    public MessagesViewModel(IEventAggregator eventAggregator, ICurrentConnection currentConnection)
    {
      MessagesUserList = new AsyncObservableCollection<MessageViewModel>();
      eventAggregator.GetEvent<ChannelNameEvent>().Subscribe(SetChannel);
      eventAggregator.GetEvent<LoginNameEvent>().Subscribe(SetClient);
      eventAggregator.GetEvent<MessageReceivedEvent>().Subscribe(AddMessage);
      _currentConnection = currentConnection;
      CommandSendMessage = new DelegateCommand(Send, () => IsAvailable).ObservesProperty(() => IsAvailable);
    }

    #endregion

    #region Methods

    public void AddMessage(MessageReceivedEventArgs eventArgs)
    {
      if (IsNotThisChannel(eventArgs))
      {
        return;
      }

      bool IsOut = _loginName != eventArgs.Author;
      string content = eventArgs.Message;
      if (Channel.Type == ChannelType.General)
      {
        content = $"{eventArgs.Author}: {content}";
      }

      var message = new MessageViewModel(content, eventArgs.Time, IsOut, true);
      MessagesUserList.Add(message);
    }

    private void Validate()
    {
      Alert.AutomaticAlert(_loginName, _currentConnection);
      IsAvailable = true;
      if (string.IsNullOrEmpty(TextMessage) || TextMessage.All(symbol => symbol == ' '))
      {
        IsAvailable = false;
      }
    }

    private void SetChannel(BaseChannel channel)
    {
      Channel = channel;
    }

    private void SetClient(string client)
    {
      _loginName = client;
    }

    private bool IsNotThisChannel(MessageReceivedEventArgs eventArgs)
    {
      if (Channel.Type != eventArgs.Agenda.Type)
      {
        return true;
      }

      switch (eventArgs.Agenda.Type)
      {
        case ChannelType.General: return false;
        case ChannelType.Private:
          if (eventArgs.Author == Channel.Name)
          {
            return false;
          }

          break;
        case ChannelType.Group:
          if (((GroupAgenda)eventArgs.Agenda).GroupName == Channel.Name)
          {
            return false;
          }

          break;
      }

      return true;
    }

    private void Send()
    {
      string author = _loginName;
      DateTime time = DateTime.Now;
      string message = TextMessage;
      var model = new MessageModel(message, time, false, true);

      switch (Channel.Type)
      {
        case ChannelType.General:
          _currentConnection.Send(new GeneralMessageContainer(author, message, _currentConnection.Type));
          break;
        case ChannelType.Private:
          _currentConnection.Send(new PrivateMessageContainer(author, Channel.Name, message, _currentConnection.Type));
          break;
      }

      Channel.MessageList.Add(model);
      MessagesUserList.Add(new MessageViewModel(model));
      TextMessage = "";
    }
    
    #endregion
  }
}
