namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System.Linq;

  using BusinessLogic.Model.ChannelsListModel;

  using EventAggregator;

  using global::Common.Network.Messages;

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

    #endregion

    #region Properties

    public BaseChannel Channel
    {
      get => _channel;
      set => SetProperty(ref _channel, value);
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

    public MessagesViewModel(IEventAggregator eventAggregator)
    {
      MessagesUserList = new AsyncObservableCollection<MessageViewModel>();
      eventAggregator.GetEvent<ChannelNameEvent>().Subscribe(SetChannel);
      eventAggregator.GetEvent<LoginNameEvent>().Subscribe(SetClient);
      eventAggregator.GetEvent<MessageReceivedEvent>().Subscribe(AddMessage);
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
      bool IsOut = _loginName != eventArgs.Author;
      string content = $"{eventArgs.Author}: {eventArgs.Message}";
      MessageViewModel message = new MessageViewModel(content, eventArgs.Time, IsOut, true);
      MessagesUserList.Add(message);
    }

    #endregion
  }
}
