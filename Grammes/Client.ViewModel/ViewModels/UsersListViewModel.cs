namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Windows.Controls;
  using System.Windows.Documents;

  using BusinessLogic.Model.UsersListModel;

  using Prism.Mvvm;

  public class UsersListViewModel : BindableBase
  {
    #region Fields

    private string _userName;

    private UsersListModel _usersList;

    private TreeView _treeViewUsers;

    private BaseUser _selectChat;

    #endregion

    #region Properties

    public string UsersName
    {
      get => _userName;
      set => SetProperty(ref _userName, "Your name: " + value);
    }

    public UsersListModel UsersList
    {
      get => _usersList;
      set => SetProperty(ref _usersList, value);
    }

    public TreeView TreeViewUsers
    {
      get => _treeViewUsers;
      set => SetProperty(ref _treeViewUsers, value);
    }

    public BaseUser SelectChat
    {
      get => _selectChat;
      set => SetProperty(ref _selectChat, value);
    }
    
    #endregion

    #region Constructors

    public UsersListViewModel()
    {
      UsersName = "User5";
      /*<Hard-Code>*/
      List<OnlineUser> onlineUsers = new List<OnlineUser>()
      {
        new OnlineUser("User1"),
        new OnlineUser("User3"),
        new OnlineUser("User13")
      };

      List<OfflineUser> offlineUsers = new List<OfflineUser>()
      {
        new OfflineUser("User2"),
        new OfflineUser("User4"),
        new OfflineUser("User24")
      }; /*</Hard-Code>*/
      UsersList = new UsersListModel(onlineUsers, offlineUsers, new List<GroupUser>());
    }

    #endregion

    #region Methods

    private List<TreeViewItem> GetTreeUsers(List<string> nameUsers)
    {
      var treeUsers = new List<TreeViewItem>();
      foreach (string item in nameUsers)
      {
        treeUsers.Add(
          new TreeViewItem
          {
            Header = item
          });
      }

      return treeUsers;
    }

    #endregion
  }
}
