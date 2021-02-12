namespace Client.ViewModel.ViewModels
{
  using System;

  using global::Common.Network;
  using global::Common.Network.Messages;

  using MessagesViewModel;

  using Prism.Commands;
  using Prism.Mvvm;

  using ViewModel.Common._Enum_;

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

    public WsClient Client { get; set; }

    #endregion

    #region Constructors

    public TotalViewModel(ConnectViewModel connectViewModel, MainViewModel mainViewModel)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      _connectViewModel = connectViewModel;
      _connectViewModel.RightSendCommand = new DelegateCommand(ExecuteChangeOnMainView);
      _mainViewModel = mainViewModel;
      _mainViewModel.MainMenuViewModel.Command = new DelegateCommand(ExecuteChangeOnConnectView);
      _mainViewModel.MessagesViewModel.CommandSendMessage = new DelegateCommand(ExecuteSendMessage);
    }

    #endregion

    #region Methods

    private void ExecuteChangeOnMainView()
    {
      Client = new WsClient(_connectViewModel.UserName);
      Client.ConnectionStateChanged += HandleConnectionStateChanged;
      Client.MessageReceived += HandleMessageReceived;
      Client.ConnectAsync(_connectViewModel.IpAddress, _connectViewModel.Port);
    }

    private void ExecuteSendMessage()
    {
      Client.Send(_mainViewModel.MessagesViewModel.TextMessage);
      _mainViewModel.MessagesViewModel.TextMessage = "";
    }
    
    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      if (string.IsNullOrEmpty(e.Message)) {
        _mainViewModel.MessagesViewModel.MessagesUserList.Add(new MessageViewModel("Connect", DateTime.Now,
          false, true));
      }
      else {
        _mainViewModel.MessagesViewModel.MessagesUserList.Add(new MessageViewModel(e.Message, DateTime.Now,
          false, true));
      }
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      if (e.Connected)
      {
        ContentPresenter = (int)ViewSelect.MainView;
      }
      else
      {
        _connectViewModel.UserName = "NO|CONNECTION";
        LostConnection();
      }
    }

    private void ExecuteChangeOnConnectView()
    {
      LostConnection();
    }

    private void LostConnection()
    {
      _mainViewModel.MessagesViewModel.MessagesUserList.Clear();
      Client.ConnectionStateChanged -= HandleConnectionStateChanged;
      Client.MessageReceived -= HandleMessageReceived;
      Client.Disconnect();

      ContentPresenter = (int)ViewSelect.ConnectView;
    }

    #endregion
  }
}
