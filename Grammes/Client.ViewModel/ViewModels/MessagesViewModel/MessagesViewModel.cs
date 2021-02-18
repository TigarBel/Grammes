namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;
  using System.Linq;

  using BusinessLogic.Model;
  using BusinessLogic.Model.ChannelsListModel;
  using BusinessLogic.Model.Network;

  using EventAggregator;

  using global::Common.Network.Messages;
  using global::Common.Network.Messages.MessageReceived;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  using ViewModel.Common;

  public class MessagesViewModel : BindableBase
  {
    #region Fields

    private string _loginName;

    private BaseChannel _channel;

    private AsyncObservableCollection<MessageViewModel> _messagesUserList;

    private DelegateCommand _commandSendMessage;

    private string _textMessage;

    private bool _isAvailable;

    private readonly IConnectionController _connectionController;

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
          MessagesUserList.Add(new MessageViewModel(message));//TODO List = List
        }
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

    public MessagesViewModel(IEventAggregator eventAggregator,
                             IConnectionController connectionController)
    {
      MessagesUserList = new AsyncObservableCollection<MessageViewModel>();
      eventAggregator.GetEvent<ChannelNameEvent>().Subscribe(SetChannel);
      eventAggregator.GetEvent<LoginNameEvent>().Subscribe(SetClient);
      eventAggregator.GetEvent<MessageReceivedEvent>().Subscribe(AddMessage);
      _connectionController = connectionController;
      CommandSendMessage = new DelegateCommand(Send);
    }

    #endregion

    #region Methods

    private void Validate()
    {
      if (string.IsNullOrEmpty(TextMessage))
      {
        IsAvailable = false;
      }

      if (TextMessage.Any(symbol => symbol != ' '))
      {
        IsAvailable = true;
        return;
      }

      IsAvailable = false;
    }

    private void SetChannel(BaseChannel channel)
    {
      Channel = channel;
    }

    private void SetClient(string client)
    {
      _loginName = client;
    }

    public void AddMessage(MessageReceivedEventArgs eventArgs)
    {
      if(IsNotThisChannel(eventArgs)) return;
      
      bool IsOut = _loginName != eventArgs.Author;
      string content = $"{eventArgs.Author}: {eventArgs.Message}";
      MessageViewModel message = new MessageViewModel(content, eventArgs.Time, IsOut, true);
      MessagesUserList.Add(message);
    }

    private bool IsNotThisChannel(MessageReceivedEventArgs eventArgs)
    {
      if (Channel.Type != eventArgs.Agenda.Type) return true;

      switch (eventArgs.Agenda.Type)
      {
        case ChannelType.General: return false;
        case ChannelType.Private:
          if (eventArgs.Author == Channel.Name) return false;
          break;
        case ChannelType.Group:
          if (((GroupAgenda)eventArgs.Agenda).GroupName == Channel.Name) return false;
          break;
      }
      return true;
    }

    private void Send()
    {
      string author = _loginName;
      DateTime time = DateTime.Now;
      string message = TextMessage;

      switch (Channel.Type)
      {
        case ChannelType.General:
          _connectionController.Send(new GeneralMessageContainer(author, message));
          break;
        case ChannelType.Private:
          _connectionController.Send(new PrivateMessageContainer(author, Channel.Name, message));
          break;
        case ChannelType.Group:

          break;
      }
    }

    #endregion
  }
}
