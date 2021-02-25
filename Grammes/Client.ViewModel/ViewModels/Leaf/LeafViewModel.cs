namespace Client.ViewModel.ViewModels.Leaf
{
  using Extension;

  using Prism.Commands;

  public abstract class LeafViewModel : ViewModelBase
  {
    #region Fields

    private string _buttonText;

    private DelegateCommand _buttonCommand;

    private bool _isAvailableButton;

    #endregion

    #region Properties
    
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

    #endregion

    #region Constructors

    protected LeafViewModel(string buttonText)
    {
      ButtonText = buttonText;
      SendCommand = null;
    }

    #endregion
  }
}
