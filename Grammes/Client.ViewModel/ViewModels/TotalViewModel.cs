namespace Client.ViewModel.ViewModels
{
  using System;
  using System.Net;

  using BusinessLogic.Model.Network;

  using global::Common.Network;
  using global::Common.Network.Messages;
  using global::Common.Network.Messages.EventLog;

  using MessagesViewModel;

  using Prism.Commands;
  using Prism.Mvvm;

  using ViewModel.Common;

  public class TotalViewModel : BindableBase
  {
    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;

    private readonly ConnectViewModel _connectViewModel;

    private readonly MainViewModel _mainViewModel;

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

    private IConnectionController _connectionController;

    #endregion

    #region Constructors

    public TotalViewModel(ConnectViewModel connectViewModel, MainViewModel mainViewModel, IConnectionController connectionController)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      _connectViewModel = connectViewModel;
      _connectViewModel.SendCommand = new DelegateCommand(ExecuteChangeOnMainView);
      _mainViewModel = mainViewModel;
      _mainViewModel.MainMenuViewModel.Command = new DelegateCommand(ExecuteChangeOnConnectView);
      _mainViewModel.MessagesViewModel.CommandSendMessage = new DelegateCommand(ExecuteSendMessage);

      _connectionController = connectionController ?? throw new ArgumentNullException(nameof(connectionController));
    }

    #endregion

    #region Methods

    private void ExecuteChangeOnMainView()
    {
      string address = _connectViewModel.IpAddress;
      int port = _connectViewModel.Port;
      string name = _connectViewModel.UserName;

      _connectionController.Connect(address, port, name);
      _connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
      _connectionController.Login += HandleLogin;
      _connectionController.MessageReceived += HandleMessageReceived;
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      if (!e.Connected)
      {
        ContentPresenter = (int)ViewSelect.ConnectView;
      }
    }

    private void HandleLogin(object sender, LoginEventArgs e)
    {
      if (e.Connected)
      {
        ContentPresenter = (int)ViewSelect.MainView;
      }
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {

    }

    private void ExecuteSendMessage()
    {

    }

    private void ExecuteChangeOnConnectView()
    {
      _connectionController.Disconnect();
      _connectionController.ConnectionStateChanged -= HandleConnectionStateChanged;
      _connectionController.Login -= HandleLogin;
      _connectionController.MessageReceived -= HandleMessageReceived;
    }
    
    #endregion
  }
}
