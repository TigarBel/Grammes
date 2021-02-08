namespace Client.ViewModel.ViewModels
{
  using Prism.Commands;
  using Prism.Mvvm;

  using ViewModel.Common._Enum_;

  public class TotalViewModel : BindableBase
  {
    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;

    private ConnectViewModel _connectViewModel;

    private MainMenuViewModel _mainMenuViewModel;

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

    public ConnectViewModel ConnectViewModel
    {
      get => _connectViewModel;
      set => SetProperty(ref _connectViewModel, value);
    }

    public MainMenuViewModel MainMenuViewModel
    {
      get => _mainMenuViewModel;
      set => SetProperty(ref _mainMenuViewModel, value);
    }

    #endregion

    #region Constructors

    public TotalViewModel(ConnectViewModel connectViewModel, MainMenuViewModel mainMenuViewModel)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      ConnectViewModel = connectViewModel;
      ConnectViewModel.RightSendCommand = new DelegateCommand(ExecuteChangeOnMainView);
      MainMenuViewModel = mainMenuViewModel;
      MainMenuViewModel.Command = new DelegateCommand(ExecuteChangeOnConnectView);
    }

    #endregion

    #region Methods

    private void ExecuteChangeOnMainView()
    {
      ContentPresenter = (int)ViewSelect.MainView;
    }

    private void ExecuteChangeOnConnectView()
    {
      ContentPresenter = (int)ViewSelect.ConnectView;
    }

    #endregion
  }
}
