namespace Client.ViewModel.ViewModels
{
  using System.Collections.ObjectModel;
  using System.Windows.Controls;

  using BusinessLogic.Model.UsersListModel;

  using EventAggregator;

  using Prism.Events;
  using Prism.Mvvm;

  public class UsersListViewModel : BindableBase
  {
    #region Fields

    private string _userName;

    private GeneralUser _general;

    private ObservableCollection<OnlineUser> _onlineUsers;

    private ObservableCollection<OfflineUser> _offlineUsers;

    private ObservableCollection<GroupUser> _groups;

    private object _selectChat;

    private readonly IEventAggregator _chatNameEa;

    #endregion

    #region Properties

    public string UsersName
    {
      get => _userName;
      set => SetProperty(ref _userName, "Your name: " + value);
    }

    public GeneralUser General
    {
      get => _general;
      set => SetProperty(ref _general, value);
    }

    public ObservableCollection<OnlineUser> OnlineUsers
    {
      get => _onlineUsers;
      set => SetProperty(ref _onlineUsers, value);
    }

    public ObservableCollection<OfflineUser> OfflineUsers
    {
      get => _offlineUsers;
      set => SetProperty(ref _offlineUsers, value);
    }

    public ObservableCollection<GroupUser> Groups
    {
      get => _groups;
      set => SetProperty(ref _groups, value);
    }

    public object SelectChat
    {
      get => _selectChat;
      set
      {
        SetProperty(ref _selectChat, value);

        switch (value)
        {
          case BaseUser user:
          {
            _chatNameEa.GetEvent<ChatNameEvent>().Publish(user.ToString());
            break;
          }
          case TreeViewItem item:
          {
            if (item.Header is BaseUser)
            {
              _chatNameEa.GetEvent<ChatNameEvent>().Publish(item.Header.ToString());
            }

            break;
          }
        }
      }
    }

    #endregion

    #region Constructors

    public UsersListViewModel(IEventAggregator eventAggregator)
    {
      _chatNameEa = eventAggregator;
      eventAggregator.GetEvent<UserNameEvent>().Subscribe(SetUserName);

      General = new GeneralUser();
      SelectChat = General;
      /*<Hard-Code>*/
      OnlineUsers = new ObservableCollection<OnlineUser>
      {
        new OnlineUser("User1"),
        new OnlineUser("User3"),
        new OnlineUser("User13")
      };
      OfflineUsers = new ObservableCollection<OfflineUser>
      {
        new OfflineUser("User2"),
        new OfflineUser("User4"),
        new OfflineUser("User24")
      };
      Groups = new ObservableCollection<GroupUser>
      {
        new GroupUser("User2"),
        new GroupUser("User4"),
        new GroupUser("User24"),
        new GroupUser("User224"),
        new GroupUser("User424"),
        new GroupUser("User2424"),
        new GroupUser("User242"),
        new GroupUser("User442"),
        new GroupUser("User2442")
      };
      /*</Hard-Code>*/

      //SelectChat = (BaseUser)TreeItemList[0];
    }

    #endregion

    #region Methods

    private void SetUserName(string userName)
    {
      UsersName = userName;
    }

    #endregion
  }
}
