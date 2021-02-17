namespace Client.ViewModel.ViewModels
{
  using System;

  using BusinessLogic.Model.Network;

  using EventAggregator;

  using global::Common.Network.Messages;

  using MessagesViewModel;

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

    public TotalViewModel(ConnectViewModel connectViewModel, 
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
      _mainViewModel.MessagesViewModel.CommandSendMessage = new DelegateCommand(ExecuteSendMessage);
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

      _mainViewModel.EventLogViewModel.Events.Add(eventArgs.EventLog);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
      //_mainViewModel.EventLogViewModel.Events.Add(e.EventLog);
      _eventAggregator.GetEvent<MessageReceivedEvent>().Publish(eventArgs);
    }

    private void ExecuteSendMessage()
    {
      string author = _connectViewModel.LoginName;
      DateTime time = DateTime.Now;
      string message = _mainViewModel.MessagesViewModel.TextMessage;
      
      _connectionController.Send(new GeneralMessageContainer(author, message));
    }

    private void ExecuteDisconnect()
    {
      _connectionController.Disconnect();
    }

    #endregion
  }
}
