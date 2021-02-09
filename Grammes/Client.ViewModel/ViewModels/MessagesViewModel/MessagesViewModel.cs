namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Windows.Input;

  using BusinessLogic.Model.UsersListModel;

  using EventAggregator;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  public class MessagesViewModel : BindableBase
  {
    #region Fields

    private string _chatName;

    private ObservableCollection<MessageViewModel> _messagesUserList;

    private DelegateCommand _commandSendMessage;

    private string _textMessage;

    private bool _isAvailable;

    #endregion

    #region Properties

    public string ChatName
    {
      get => "Chat name: " + _chatName;
      set => SetProperty(ref _chatName, value);
    }

    public ObservableCollection<MessageViewModel> MessagesUserList
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
      MessagesUserList = new ObservableCollection<MessageViewModel>();
      CommandSendMessage = new DelegateCommand(ExecuteSendMessage);
      eventAggregator.GetEvent<ChatNameEvent>().Subscribe(SetChatName);
    }

    #endregion

    #region Methods

    private void ExecuteSendMessage()
    {
      MessagesUserList.Add(new MessageViewModel(TextMessage,
        new DateTime(2001, 10, 10, 10, 10, 10), false, true));
      TextMessage = "";
    }

    private void Validate()
    {
      if (string.IsNullOrEmpty(TextMessage)) {
        IsAvailable = false;
      }

      if (TextMessage.Any(symbol => symbol != ' '))
      {
        IsAvailable = true;
        return;
      }

      IsAvailable = false;
    }

    private void SetChatName(string chatName)
    {
      ChatName = chatName;
    }

    #endregion
  }
}
