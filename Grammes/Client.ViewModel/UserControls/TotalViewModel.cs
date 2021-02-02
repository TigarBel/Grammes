namespace Client.ViewModel.UserControls
{
  using System.Runtime.Remoting.Contexts;
  using System.Windows.Controls;

  using Prism.Commands;
  using Prism.Mvvm;

  public class TotalViewModel : BindableBase
  {

    #region Fields

    private int _contentPresenter;

    private string[] _nameViews;

    private ConnectViewModel _connectViewModel;

    #endregion

    #region Propertyes

    public int ContentPresenter
    {
      get => _contentPresenter;
      set
      {
        SetProperty(ref _contentPresenter, value);
      }
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
      ConnectViewModel.RightSendCommand = new DelegateCommand(OnChange);
    }

    #endregion

    #region Methods

    public void OnChange()
    {
      if (ContentPresenter == NameViews.Length - 1)
      {
        ContentPresenter = 0;
      }
      else
      {
        ContentPresenter++;
      }
    }

    #endregion

  }
}
