﻿namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Documents;

  using BusinessLogic.Model.UsersListModel;

  using Prism.Mvvm;
  

  public class UsersListViewModel : BindableBase
  {
    #region Fields

    private string _userName;

    private UsersListModel _usersList;

    private BaseUser _selectChat;

    private List<object> _treeItemList;

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

    public List<object> TreeItemList
    {
      get => _treeItemList;
      set => SetProperty(ref _treeItemList, value);
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
      };
      List<GroupUser> group = new List<GroupUser>()
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

      UsersList = new UsersListModel(onlineUsers, offlineUsers, group); /*</Hard-Code>*/

      TreeItemList = new List<object>()
      {
          UsersList.General,
          new TreeViewItem()
          {
            Header = "Online",
            ItemsSource = UsersList.OnlineList
          },
          new TreeViewItem()
          {
            Header = "Offline",
            ItemsSource = UsersList.OfflineList
          },
          new TreeViewItem()
          {
            Header = "Group",
            ItemsSource = UsersList.GroupList
          }
      };
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
