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

    private NetworkClient _client;

    #endregion

    #region Constructors

    public TotalViewModel(ConnectViewModel connectViewModel, MainViewModel mainViewModel)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      _connectViewModel = connectViewModel;
      _connectViewModel.SendCommand = new DelegateCommand(ExecuteChangeOnMainView);
      _mainViewModel = mainViewModel;
      _mainViewModel.MainMenuViewModel.Command = new DelegateCommand(ExecuteChangeOnConnectView);
      _mainViewModel.MessagesViewModel.CommandSendMessage = new DelegateCommand(ExecuteSendMessage);
    }

    #endregion

    #region Methods

    private void ExecuteChangeOnMainView()
    {
      string name = _connectViewModel.UserName;
      IPAddress address = IPAddress.Parse(_connectViewModel.IpAddress);
      int port = _connectViewModel.Port;
      IPEndPoint ipEndPoin = new IPEndPoint(address, port);
      _client = new NetworkClient(name, ipEndPoin);

      ContentPresenter = (int)ViewSelect.MainView;
    }

    private void ExecuteSendMessage()
    {

    }

    private void ExecuteChangeOnConnectView()
    {
      ContentPresenter = (int)ViewSelect.ConnectView;
      _client.LostConnection();
    }

    #endregion
  }
}
