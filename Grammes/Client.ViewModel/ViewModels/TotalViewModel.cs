namespace Client.ViewModel.ViewModels
{
  using System;

  using Prism.Commands;
  using Prism.Mvvm;

  using ViewModel.Common._Enum_;

  using WebSocketSharp;

  public class TotalViewModel : BindableBase
  {
    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;

    private readonly ConnectViewModel _connectViewModel;

    private readonly MainMenuViewModel _mainMenuViewModel;

    private UsersListViewModel _usersListViewModel;

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

    public TotalViewModel(ConnectViewModel connectViewModel, MainMenuViewModel mainMenuViewModel, UsersListViewModel usersListViewModel)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      _connectViewModel = connectViewModel;
      _connectViewModel.RightSendCommand = new DelegateCommand(ExecuteChangeOnMainView);
      _mainMenuViewModel = mainMenuViewModel;
      _mainMenuViewModel.Command = new DelegateCommand(ExecuteChangeOnConnectView);
      _usersListViewModel = usersListViewModel;
    }

    #endregion

    #region Methods

    private void ExecuteChangeOnMainView()
    {
      ContentPresenter = (int)ViewSelect.MainView;
      CreateClient();
    }

    private void CreateClient()
    {
      var client = new WebSocket("ws://192.168.37.228:65000/User1");
      client.OnMessage += Client_OnMessage;
      client.Connect();

      Console.WriteLine("Enter message:");

      client.Send("Hi!");
    }

    private static void Client_OnMessage(object sender, MessageEventArgs e)
    {
      Console.WriteLine(e.Data);
      Console.WriteLine("Enter message:");
    }

    private void ExecuteChangeOnConnectView()
    {
      ContentPresenter = (int)ViewSelect.ConnectView;
    }

    #endregion
  }
}
