﻿namespace Client.ViewModel.ViewModels
{
  using System;
  using System.Linq;
  using System.Windows.Controls;

  using BusinessLogic.Model.Network;

  using Common.Network.ChannelsListModel;
  using Common.Network.ChannelsListModel.BaseUserChannel;
  using Common.Network.Messages;
  using Common.Network.Messages.EventLog;

  using EventAggregator;

  using Extension;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  public class TotalViewModel : BindableBase
  {
    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;

    private readonly ConnectViewModel _connectViewModel;

    private readonly MainViewModel _mainViewModel;

    private readonly IConnectionController _connectionController;

    private readonly IEventAggregator _eventAggregator;

    #endregion

    #region Properties

    public int ContentPresenter
    {
      get => _contentPresenter;
      set => SetProperty(ref _contentPresenter, value);
    }

    public string[] NameViews
    {
      get => _nameViews;
      set => SetProperty(ref _nameViews, value);
    }

    #endregion

    #region Constructors

    public TotalViewModel(
      ConnectViewModel connectViewModel,
      MainViewModel mainViewModel,
      IConnectionController connectionController,
      IEventAggregator eventAggregator)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      _connectViewModel = connectViewModel;
      _connectViewModel.SendCommand = new DelegateCommand(ExecuteConnect);
      _mainViewModel = mainViewModel;
      _mainViewModel.MainMenuViewModel.Command = new DelegateCommand(ExecuteDisconnect);
      _eventAggregator = eventAggregator;

      _connectionController = connectionController ?? throw new ArgumentNullException(nameof(connectionController));
    }

    #endregion

    #region Methods

    private void ExecuteConnect()
    {
      _connectViewModel.Warning = "";
      _mainViewModel.Clear();

      string address = _connectViewModel.IpAddress;
      int port = _connectViewModel.Port;
      string name = _connectViewModel.LoginName;

      _connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
      _connectionController.Login += HandleLogin;
      _connectionController.MessageReceived += HandleMessageReceived;
      _connectionController.UpdateChannel += HandleUpdateChannel;
      _connectionController.Connect(address, port, name);
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs eventArgs)
    {
      _mainViewModel.Events.Add(eventArgs.EventLog);

      if (eventArgs.Connected)
      {
        _connectViewModel.IsAvailableButton = false;
      }
      else
      {
        _connectViewModel.IsAvailableButton = true;
        if (ContentPresenter == (int)ViewSelect.ConnectView)
        {
          _connectViewModel.Warning = $"{eventArgs.EventLog.Text}";
        }
        else
        {
          ContentPresenter = (int)ViewSelect.ConnectView;
        }

        _connectionController.ConnectionStateChanged -= HandleConnectionStateChanged;
        _connectionController.Login -= HandleLogin;
        _connectionController.MessageReceived -= HandleMessageReceived;
        _connectionController.UpdateChannel -= HandleUpdateChannel;
      }
    }

    private void HandleLogin(object sender, LoginEventArgs eventArgs)
    {
      _mainViewModel.Events.Add(eventArgs.EventLog);

      if (eventArgs.Connected)
      {
        ContentPresenter = (int)ViewSelect.MainView;
      }
      else
      {
        _connectionController.Disconnect();
        _connectViewModel.Warning = $"{eventArgs.EventLog.Text}";
        return;
      }

      _mainViewModel.UsersListViewModel.General.MessageList = eventArgs.General.MessageList;
      _mainViewModel.UsersListViewModel.OnInit();

      foreach (var privateChannel in eventArgs.OnlineList)
      {
        _mainViewModel.UsersListViewModel.OnlineUsers.Add(privateChannel);
      }

      foreach (var privateChannel in eventArgs.OfflineList)
      {
        _mainViewModel.UsersListViewModel.OfflineUsers.Add(privateChannel);
      }
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
       var eventLog = new EventLogMessage(eventArgs.Author, true, DispatchType.Message, 
        $"{eventArgs.Agenda.Type}:{eventArgs.Message}", eventArgs.Time);
      _mainViewModel.Events.Add(eventLog);
      _eventAggregator.GetEvent<MessageReceivedEvent>().Publish(eventArgs);
    }

    private void HandleUpdateChannel(object sender, UpdateChannelEventArgs eventArgs)
    {
      _mainViewModel.Events.Add(eventArgs.EventLog);
      string selfLogin = _connectViewModel.LoginName;
      string comeLogin = eventArgs.ChannelName;

      if (eventArgs.IsNewLogin)
      {
        _mainViewModel.UsersListViewModel.OnlineUsers.Add(new OnlineChannel(comeLogin));
        return;
      }

      if (eventArgs.Connected)
      {
        ChangeChannel(comeLogin, _mainViewModel.UsersListViewModel.OfflineUsers, 
          _mainViewModel.UsersListViewModel.OnlineUsers);
      }
      else
      {
        ChangeChannel(comeLogin, _mainViewModel.UsersListViewModel.OnlineUsers, 
          _mainViewModel.UsersListViewModel.OfflineUsers);
        SwitchChannel(comeLogin);
      }
    }

    private void ChangeChannel(string comeLogin, 
                               AsyncObservableCollection<PrivateChannel> channelsOf,
                               AsyncObservableCollection<PrivateChannel> channelsIn)
    {
      PrivateChannel oldChannel = channelsOf.Single(u => u.Name == comeLogin);
      var newChannel = new PrivateChannel(comeLogin)
      {
        MessageList = oldChannel.MessageList
      };
      channelsIn.Add(newChannel);
      channelsOf.Remove(oldChannel);
    }

    private void SwitchChannel(string comeLogin)
    {
      if (_mainViewModel.MessagesViewModel.Channel.Name == comeLogin)
        _mainViewModel.UsersListViewModel.SelectChat = _mainViewModel.UsersListViewModel.General;
    }

    private void ExecuteDisconnect()
    {
      _connectionController.Disconnect();
    }

    #endregion
  }
}
