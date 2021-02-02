namespace Client.ViewModel.UserControls.Common
{
  using Prism.Commands;

  using ViewModel.Common;

  public class LeafViewModel : ViewModelBase
  {
    #region Constants

    #endregion

    #region Fields

    private string _leftButtonText;

    private string _rightButtonText;

    private DelegateCommand _leftButtonCommand;

    private DelegateCommand _rightButtonCommand;

    private bool _isAvailableLeftButton;

    private bool _isAvailableRightButton;

    #endregion

    #region Properties

    public string LeftButtonText
    {
      get => _leftButtonText;
      set => SetProperty(ref _leftButtonText, value);
    }

    public string RightButtonText
    {
      get => _rightButtonText;
      set => SetProperty(ref _rightButtonText, value);
    }

    public DelegateCommand LeftSendCommand
    {
      get => _leftButtonCommand;
      set => SetProperty(ref _leftButtonCommand, value);
    }
    
    public DelegateCommand RightSendCommand
    {
      get => _rightButtonCommand;
      set => SetProperty(ref _rightButtonCommand, value);
    }

    public bool IsAvailableLeftButton
    {
      get => _isAvailableLeftButton;
      set => SetProperty(ref _isAvailableLeftButton, value);
    }

    public bool IsAvailableRightButton
    {
      get => _isAvailableRightButton;
      set => SetProperty(ref _isAvailableRightButton, value);
    }

    #endregion

    #region Constructors

    public LeafViewModel(string buttonText)
    {
      LeftButtonText = null;
      LeftSendCommand = null;

      RightButtonText = buttonText;
      RightSendCommand = null;
    }

    public LeafViewModel(string leftButtonText, string rightButtonText)
    {
      LeftButtonText = leftButtonText;
      RightButtonText = rightButtonText;
      LeftSendCommand = null;
      RightSendCommand = null;
    }

    public override void Check()
    {
      throw new System.NotImplementedException();
    }

    #endregion

  }
}
