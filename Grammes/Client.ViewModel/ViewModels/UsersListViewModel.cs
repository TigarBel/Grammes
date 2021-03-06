﻿namespace Client.ViewModel.ViewModels
{
  using System.Linq;
  using System.Windows.Controls;

  using Common.Network.ChannelsListModel;
  using Common.Network.Messages;
  using Common.Network.Messages.MessageReceived;

  using EventAggregator;

  using Extension;

  using Prism.Events;
  using Prism.Mvvm;

  public class UsersListViewModel : BindableBase
  {
    #region Fields

    private string _userName;

    private GeneralChannel _general;

    private AsyncObservableCollection<PrivateChannel> _onlineUsers;

    private AsyncObservableCollection<PrivateChannel> _offlineUsers;

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

    public AsyncObservableCollection<PrivateChannel> OnlineUsers
    {
      get => _onlineUsers;
      set => SetProperty(ref _onlineUsers, value);
    }

    public AsyncObservableCollection<PrivateChannel> OfflineUsers
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
      OnlineUsers = new AsyncObservableCollection<PrivateChannel>();
      OfflineUsers = new AsyncObservableCollection<PrivateChannel>();
      Groups = new AsyncObservableCollection<GroupChannel>();
    }

    #endregion

    #region Methods

    public void OnInit()
    {
      OnlineUsers.Clear();
      OfflineUsers.Clear();
      Groups.Clear();
      SelectChat = General;
    }

    private void SetUserName(string userName)
    {
      UsersName = userName;
    }

    private void AddMessageOnChannel(MessageReceivedEventArgs eventArgs)
    {
      var message = new MessageModel(eventArgs.Message, eventArgs.Time, true, true);

      switch (eventArgs.Agenda.Type)
      {
        case ChannelType.General:
          message.Message = $"{eventArgs.Author}: {message.Message}";
          General.MessageList.Add(message);
          break;
        case ChannelType.Private:
          foreach (PrivateChannel channel in OnlineUsers.Where(user => user.Name == eventArgs.Author))
          {
            channel.MessageList.Add(message);
          }

          break;
        case ChannelType.Group:
          GroupChannel groupChannel = Groups.Where(item => item.Name == ((GroupAgenda)eventArgs.Agenda).GroupName).GetEnumerator().Current;
          groupChannel?.MessageList.Add(message);
          break;
      }
    }

    #endregion
  }
}
