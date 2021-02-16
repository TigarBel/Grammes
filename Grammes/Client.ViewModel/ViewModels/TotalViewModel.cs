namespace Client.ViewModel.ViewModels
{
  using System;

  using BusinessLogic.Model.Network;

  using global::Common.Network.Messages;

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

    private readonly IConnectionController _connectionController;

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

    public TotalViewModel(ConnectViewModel connectViewModel, MainViewModel mainViewModel, IConnectionController connectionController)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      _connectViewModel = connectViewModel;
      _connectViewModel.SendCommand = new DelegateCommand(ExecuteConnect);
      _mainViewModel = mainViewModel;
      _mainViewModel.MainMenuViewModel.Command = new DelegateCommand(ExecuteDisconnect);
      _mainViewModel.MessagesViewModel.CommandSendMessage = new DelegateCommand(ExecuteSendMessage);

      _connectionController = connectionController ?? throw new ArgumentNullException(nameof(connectionController));
    }

    #endregion

    #region Methods

    private void ExecuteConnect()
    {
      _connectViewModel.Warning = "";
      _mainViewModel.EventLogViewModel.Events.Clear();
      
      string address = _connectViewModel.IpAddress;
      int port = _connectViewModel.Port;
      string name = _connectViewModel.UserName;

      _connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
      _connectionController.Login += HandleLogin;
      _connectionController.MessageReceived += HandleMessageReceived;
      _connectionController.Connect(address, port, name);
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      if (!e.Connected)
      {
        if (ContentPresenter == (int)ViewSelect.ConnectView)
        {
          _connectViewModel.Warning = $"{e.EventLog.Text}";
        }
        else
        {
          ContentPresenter = (int)ViewSelect.ConnectView;
        }
        
        _connectionController.ConnectionStateChanged -= HandleConnectionStateChanged;
        _connectionController.Login -= HandleLogin;
        _connectionController.MessageReceived -= HandleMessageReceived;
      }

      _mainViewModel.EventLogViewModel.Events.Add(e.EventLog);
    }

    private void HandleLogin(object sender, LoginEventArgs e)
    {
      if (e.Connected)
      {
        ContentPresenter = (int)ViewSelect.MainView;
      }
      else
      {
        _connectionController.Disconnect();
        _connectViewModel.Warning = $"{e.EventLog.Text}";
      }

      _mainViewModel.EventLogViewModel.Events.Add(e.EventLog);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      //_mainViewModel.EventLogViewModel.Events.Add(e.EventLog);
      _mainViewModel.MessagesViewModel.MessagesUserList.Add(
        new MessageViewModel(e.Message, e.Time,true,true));
    }

    private void ExecuteSendMessage()
    {
      string author = _connectViewModel.UserName;
      DateTime time = DateTime.Now;
      string message = _mainViewModel.MessagesViewModel.TextMessage;
      _mainViewModel.MessagesViewModel.MessagesUserList.Add(
        new MessageViewModel(message, time, false, true));
      _connectionController.Send(new GeneralMessageRequestContainer(author, time, message));
    }

    private void ExecuteDisconnect()
    {
      _connectionController.Disconnect();
    }

    #endregion
  }
}
