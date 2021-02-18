namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows.Controls;

  using BusinessLogic.Model;
  using BusinessLogic.Model.ChannelsListModel;
  using BusinessLogic.Model.ChannelsListModel.BaseUserChannel;

  using EventAggregator;

  using global::Common.Network.Messages;
  using global::Common.Network.Messages.MessageReceived;

  using MessagesViewModel;

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
      set => SetProperty(ref _userName, value);
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
            if (item.Header is BaseChannel channel)
            {
              _chatNameEa.GetEvent<ChannelNameEvent>().Publish(channel);
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
      eventAggregator.GetEvent<MessageReceivedEvent>().Subscribe(AddMessageOnChannel);

      General = new GeneralChannel();
      OnlineUsers = new AsyncObservableCollection<OnlineChannel>();
      OfflineUsers = new AsyncObservableCollection<OfflineChannel>();
      Groups = new AsyncObservableCollection<GroupChannel>();

      SelectChat = General;
    }

    #endregion

    #region Methods

    private void SetUserName(string userName)
    {
      UsersName = userName;
    }

    private void AddMessageOnChannel(MessageReceivedEventArgs eventArgs)
    {
      bool IsOut = _userName != eventArgs.Author;
      string content = $"{eventArgs.Author}: {eventArgs.Message}";
      MessageModel message = new MessageModel(content, eventArgs.Time, IsOut, true);

      switch (eventArgs.Agenda.Type)
      {
        case ChannelType.General:
          General.MessageList.Add(message);
          break;
        case ChannelType.Private:
          if (eventArgs.Author == _userName)
          {
            OnlineChannel onlineChannel = OnlineUsers.Where(userItem => 
              userItem.Name == ((PrivateAgenda)eventArgs.Agenda).Target).GetEnumerator().Current;
            onlineChannel?.MessageList.Add(message);
          }
          else
          {
            OnlineChannel onlineChannel = OnlineUsers.Where(userItem => 
              userItem.Name == eventArgs.Author).GetEnumerator().Current;
            onlineChannel?.MessageList.Add(message);
          }

          break;
        case ChannelType.Group:
          GroupChannel groupChannel = Groups.Where(
            item => item.Name == ((GroupAgenda)eventArgs.Agenda).GroupName).GetEnumerator().Current;
          groupChannel?.MessageList.Add(message);
          break;
      }
    }
    #endregion
  }
}
