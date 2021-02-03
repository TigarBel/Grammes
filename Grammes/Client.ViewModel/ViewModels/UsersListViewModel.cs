namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Windows.Controls;

  using BusinessLogic.Model.UsersListModel;

  using Prism.Mvvm;

  public class UsersListViewModel : BindableBase
  {
    #region Fields

    private string _userName;

    private UsersListModel _usersList;

    private TreeViewItem _selectChat;

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

    public TreeViewItem SelectChat
    {
      get => _selectChat;
      set => SetProperty(ref _selectChat, value);
    }

    public List<TreeViewItem> TreeUsersOnline => GetTreeUsers(UsersList.OnlineList);

    public List<TreeViewItem> TreeUsersOffline => GetTreeUsers(UsersList.OfflineList);

    public List<TreeViewItem> TreeGroup => GetTreeUsers(UsersList.GroupList);

    #endregion

    #region Constructors

    public UsersListViewModel()
    {
      UsersName = "User5";
      UsersList = new UsersListModel();
      /*<Hard-Code>*/
      UsersList.OnlineList = new List<string>
      {
        "User1",
        "User3",
        "User13"
      };

      UsersList.OfflineList = new List<string>
      {
        "User2",
        "User4",
        "User24"
      }; /*</Hard-Code>*/
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
