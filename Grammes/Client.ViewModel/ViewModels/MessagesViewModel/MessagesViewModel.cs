namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;
  using System.Collections.ObjectModel;

  using BusinessLogic.Model.UsersListModel;

  using Prism.Commands;
  using Prism.Mvvm;

  public class MessagesViewModel : BindableBase
  {
    #region Fields

    private string _chatName;

    private ObservableCollection<MessageViewModel> _messagesUserList;

    private DelegateCommand _command;

    private string _textMessage;

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

    public DelegateCommand Command
    {
      get => _command;
      set => SetProperty(ref _command, value);
    }

    public string TextMessage
    {
      get => _textMessage;
      set => SetProperty(ref _textMessage, value);
    }

    #endregion

    #region Constructors

    public MessagesViewModel()
    {
      MessagesUserList = new ObservableCollection<MessageViewModel>();
      Command = new DelegateCommand(ExecuteSendMessage);
    }

    #endregion

    #region Methods

    private void ExecuteSendMessage()
    {
      if (IsEmpty(TextMessage))
      {
        return;
      }

      MessagesUserList.Add(new MessageViewModel(TextMessage, new DateTime(2001, 10, 10, 10, 10, 10), false, true));
    }

    private bool IsEmpty(string text)
    {
      if (string.IsNullOrEmpty(text))
      {
        return true;
      }

      foreach (char symbol in text)
      {
        if (symbol != ' ')
        {
          return false;
        }
      }

      return true;
    }

    #endregion
  }
}
