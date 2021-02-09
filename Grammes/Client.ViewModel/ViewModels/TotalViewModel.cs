namespace Client.ViewModel.ViewModels
{
  using EventAggregator;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  using ViewModel.Common._Enum_;

  public class TotalViewModel : BindableBase
  {
    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;

    private ConnectViewModel _connectViewModel;

    private MainMenuViewModel _mainMenuViewModel;

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

    public TotalViewModel(ConnectViewModel connectViewModel, MainMenuViewModel mainMenuViewModel,
                          UsersListViewModel usersListViewModel)
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
    }

    private void ExecuteChangeOnConnectView()
    {
      ContentPresenter = (int)ViewSelect.ConnectView;
    }

    #endregion
  }
}
