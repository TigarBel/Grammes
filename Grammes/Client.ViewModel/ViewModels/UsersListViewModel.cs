namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Windows.Controls;

  using BusinessLogic.Model.ChannelsListModel;
  using BusinessLogic.Model.ChannelsListModel.BaseUserChannel;

  using EventAggregator;

  using Prism.Events;
  using Prism.Mvvm;

  using ViewModel.Common;

  public class UsersListViewModel : BindableBase
  {
    #region Fields

    private string _userName;

    private GeneralChannel _general;

    private AsyncObservableCollection<OnlineChannel> _onlineUsers;

    private AsyncObservableCollection<OfflineChannel> _offlineUsers;

    private AsyncObservableCollection<GroupChannel> _groups;

    private object _selectChat;

    private readonly IEventAggregator _chatNameEa;

    #endregion

    #region Properties

    public string UsersName
    {
      get => _userName;
      set => SetProperty(ref _userName, "Your name: " + value);
    }

    public GeneralChannel General
    {
      get => _general;
      set => SetProperty(ref _general, value);
    }

    public AsyncObservableCollection<OnlineChannel> OnlineUsers
    {
      get => _onlineUsers;
      set => SetProperty(ref _onlineUsers, value);
    }

    public AsyncObservableCollection<OfflineChannel> OfflineUsers
    {
      get => _offlineUsers;
      set => SetProperty(ref _offlineUsers, value);
    }

    public AsyncObservableCollection<GroupChannel> Groups
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
          case BaseChannel user:
          {
            _chatNameEa.GetEvent<ChannelNameEvent>().Publish(user);
            break;
          }
          case TreeViewItem item:
          {
            if (item.Header is BaseChannel)
            {
              _chatNameEa.GetEvent<ChannelNameEvent>().Publish((BaseChannel)item.Header);
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
      eventAggregator.GetEvent<LoginNameEvent>().Subscribe(SetUserName);

      General = new GeneralChannel();
      SelectChat = General;
      /*<Hard-Code>*/
      OnlineUsers = new AsyncObservableCollection<OnlineChannel>
      {
        new OnlineChannel("User1"),
        new OnlineChannel("User3"),
        new OnlineChannel("User13")
      };
      OfflineUsers = new AsyncObservableCollection<OfflineChannel>
      {
        new OfflineChannel("User2"),
        new OfflineChannel("User4"),
        new OfflineChannel("User24")
      };
      Groups = new AsyncObservableCollection<GroupChannel>
      {
        new GroupChannel(
          "User2",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User4",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User24",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User224",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User424",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User2424",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User242",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User442",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          }),
        new GroupChannel(
          "User2442",
          new List<UserChannel>
          {
            new OfflineChannel("User1"),
            new OfflineChannel("User1")
          })
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
