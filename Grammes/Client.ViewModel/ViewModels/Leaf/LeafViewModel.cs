namespace Client.ViewModel.ViewModels.Leaf
{
  using Extension;

  using Prism.Commands;

  using View.UserControls.Themes;

  public abstract class LeafViewModel : ViewModelBase
  {
    #region Fields

    private string _buttonText;

    private DelegateCommand _buttonCommand;

    private bool _isAvailableButton; 

    private bool _isFilling;

    #endregion

    #region Properties

    public ThemeType Theme { get; set; }

    public string ButtonText
    {
      get => _buttonText;
      set => SetProperty(ref _buttonText, value);
    }
    
    public DelegateCommand SendCommand
    {
      get => _buttonCommand;
      set => SetProperty(ref _buttonCommand, value);
    }
    
    public bool IsAvailableButton
    {
      get => _isAvailableButton;
      set => SetProperty(ref _isAvailableButton, value);
    }

    public bool IsFilling
    {
      get => _isFilling;
      set => SetProperty(ref _isFilling, value);
    }

    #endregion

    #region Constructors

    protected LeafViewModel(string buttonText)
    {
      ButtonText = buttonText;
      SendCommand = null;
      Theme = ThemeType.White;
      IsFilling = true;
    }

    #endregion
  }
}
