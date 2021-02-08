using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.ViewModels
{
  using System.ComponentModel;

  using BusinessLogic.Model.UsersListModel;

  using Prism.Commands;
  using Prism.Mvvm;

  public class UsersListMessagesViewModel : BindableBase
  {
    private UsersListViewModel _usersList;

    private MessagesViewModel.MessagesViewModel _messages;

    public UsersListViewModel UsersList
    {
      get => _usersList;
      set => SetProperty(ref _usersList, value);
    }

    public MessagesViewModel.MessagesViewModel Messages
    {
      get => _messages;
      set => SetProperty(ref _messages, value);
    }

    private BaseUser _chatName;
    public BaseUser ChatName
    {
      get => _chatName;
      set => SetProperty(ref _chatName, value);
    }

    public UsersListMessagesViewModel(UsersListViewModel usersList, MessagesViewModel.MessagesViewModel messages)
    {
      UsersList = usersList;
      Messages = messages;
    }
  }
}
