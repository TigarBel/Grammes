namespace Client.ViewModel.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using BusinessLogic.Model.ChannelsListModel.BaseUserChannel;
  using BusinessLogic.Model.Network;

  using EventAggregator;

  using global::Common.Network.Messages;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  using ViewModel.Common;

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
      _connectionController.Connect(address, port, name);
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs eventArgs)
    {
      string selfLogin = _connectViewModel.LoginName;
      string comeLogin = eventArgs.ClientName;

      if (selfLogin != comeLogin)
      {
        if (eventArgs.Connected)
        {
          _mainViewModel.UsersListViewModel.OnlineUsers.Add(new OnlineChannel(comeLogin));
          OfflineChannel user = _mainViewModel.UsersListViewModel.OfflineUsers.Single(offline => offline.Name == comeLogin);
          _mainViewModel.UsersListViewModel.OfflineUsers.Remove(user);
        }
        else
        {
          _mainViewModel.UsersListViewModel.OfflineUsers.Add(new OfflineChannel(comeLogin));
          OnlineChannel user = _mainViewModel.UsersListViewModel.OnlineUsers.Single(online => online.Name == comeLogin);
          _mainViewModel.UsersListViewModel.OnlineUsers.Remove(user);
        }

        return;
      }

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
      }

      _mainViewModel.EventLogViewModel.Events.Add(eventArgs.EventLog);
    }

    private void HandleLogin(object sender, LoginEventArgs eventArgs)
    {
      if (eventArgs.Connected)
      {
        ContentPresenter = (int)ViewSelect.MainView;
      }
      else
      {
        _connectionController.Disconnect();
        _connectViewModel.Warning = $"{eventArgs.EventLog.Text}";
      }

      _mainViewModel.UsersListViewModel.OnlineUsers.Clear();
      _mainViewModel.UsersListViewModel.OfflineUsers.Clear();
      _mainViewModel.UsersListViewModel.Groups.Clear();
      
      foreach (string online in eventArgs.OnlineList.Where(online => _connectViewModel.LoginName != online))
      {
        _mainViewModel.UsersListViewModel.OnlineUsers.Add(new OnlineChannel(online));
      }
      
      foreach (string offline in eventArgs.OfflineList) {
        _mainViewModel.UsersListViewModel.OfflineUsers.Add(new OfflineChannel(offline));
      }

      _mainViewModel.EventLogViewModel.Events.Add(eventArgs.EventLog);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
      //_mainViewModel.EventLogViewModel.Events.Add(e.EventLog);
      _eventAggregator.GetEvent<MessageReceivedEvent>().Publish(eventArgs);
    }
    
    private void ExecuteDisconnect()
    {
      _connectionController.Disconnect();
    }

    #endregion
  }
}
