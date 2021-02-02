namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Text.RegularExpressions;

  using BusinessLogic.UserControls.Common._Enum_;

  using Common;

  using Prism.Commands;

  public class ConnectViewModel : LeafViewModel
  {
    #region Constants

    private const string LEFT_BUTTON_TEXT = "Test";

    private const string RIGHT_BUTTON_TEXT = "Connect";

    #endregion

    #region Fields

    private string _ipAddress;

    private int _port;

    private string _selectTypeInterface;

    private List<string> _typeInterfaceList = new List<string>
    {
      InterfaceType.WebSocet.ToString(),
      InterfaceType.TcpSocet.ToString()
    };

    private string _userName;

    private Regex _regexIP = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

    private Regex _regexLogin = new Regex(@"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$");

    #endregion

    #region Properties

    public List<string> TypeInterfaceList
    {
      get => _typeInterfaceList;
      set => SetProperty(ref _typeInterfaceList, value);
    }

    public string IpAddress
    {
      get => _ipAddress;
      set => SetProperty(ref _ipAddress, value);
    }

    public int Port
    {
      get => _port;
      set => SetProperty(ref _port, value);
    }

    public string SelectTypeInterface
    {
      get => _selectTypeInterface;
      set => SetProperty(ref _selectTypeInterface, value);
    }

    public string UserName
    {
      get => _userName;
      set
      {
        SetProperty(ref _userName, value);
        Check();
      }
    }

    #endregion

    #region Constructors

    public ConnectViewModel()
      : base(LEFT_BUTTON_TEXT, RIGHT_BUTTON_TEXT)
    {
      LeftSendCommand = new DelegateCommand(ExecuteSendCommandTest);
      IsAvailableLeftButton = true;
      UserName = "";
      SelectTypeInterface = InterfaceType.TcpSocet.ToString();
    }

    #endregion

    #region Methods

    public override void Check()
    {
      _errorsContainer.ClearErrors(() => UserName);

      if (UserName.Length == 0)
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "User not entered" });
      }

      if (UserName.Length > 16)
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "Username up to 16 characters" });
      }

      //if (!IsNameUnique()) {
      //  _errorsContainer.SetErrors(() => UserName, new[] { Resources.NameMustBeUnique });
      //}

      //if (!new Regex(UserSettings.USERNAME_MASK, RegexOptions.IgnoreCase).IsMatch(UserName)) {
      //  _errorsContainer.SetErrors(
      //    () => UserName,
      //    new[] { string.Format(Resources.UserNameUnacceptableSymbols, UserSettings.USERNAME_UNACCEPTABLE_SYMBOLS) });
      //}

      if (_errorsContainer.GetErrors().Count == 0)
      {
        IsAvailableRightButton = true;
      }
      else
      {
        IsAvailableRightButton = false;
      }
    }

    private void ExecuteSendCommandTest()
    {
      IpAddress = "192.168.0.1";
      Port = 3000;
      SelectTypeInterface = InterfaceType.TcpSocet.ToString();
      UserName = "User1";
    }

    private void IsAvailable()
    {
      IsAvailableRightButton = true;
    }

    #endregion
  }
}
