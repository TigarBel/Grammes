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

    #endregion

    #region Constructors

    public TotalViewModel(ConnectViewModel connectViewModel)
    {
      NameViews = new TemplateSelectorViewModel().Views;
      ContentPresenter = 0;
      ConnectViewModel = connectViewModel;
      ConnectViewModel.RightSendCommand = new DelegateCommand(ExecuteChangeOnMainView);
    }

    #endregion

    #region Methods

    private void ExecuteChangeOnMainView()
    {
      ContentPresenter = (int)ViewSelect.MainView;
    }

    #endregion
  }
}
