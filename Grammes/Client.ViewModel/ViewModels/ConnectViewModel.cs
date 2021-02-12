namespace Client.ViewModel.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Text.RegularExpressions;

  using BusinessLogic.Model.Common;

  using Common;

  using EventAggregator;

  using Prism.Commands;
  using Prism.Events;

  public class ConnectViewModel : LeafViewModel
  {
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

    private readonly Regex _regexIP = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

    private readonly Regex _regexLogin = new Regex(@"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$");

    private readonly IEventAggregator _userNameEa;

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
      set
      {
        SetProperty(ref _ipAddress, value);
        Check();
      }
    }

    public int Port
    {
      get => _port;
      set
      {
        SetProperty(ref _port, value);
        Check();
      }
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
        _userNameEa.GetEvent<UserNameEvent>().Publish(_userName);
      }
    }

    #endregion

    #region Constructors

    public ConnectViewModel(IEventAggregator eventAggregator)
      : base("Test", "Connect")
    {
      _userNameEa = eventAggregator;
      LeftSendCommand = new DelegateCommand(ExecuteSendCommandTest);
      IsAvailableLeftButton = true;
      IpAddress = "192.168.37.228";
      Port = 3000;
      UserName = "";
      SelectTypeInterface = InterfaceType.WebSocet.ToString();
    }

    #endregion

    #region Methods

    public override void Check()
    {
      _errorsContainer.ClearErrors(() => IpAddress);
      _errorsContainer.ClearErrors(() => Port);
      _errorsContainer.ClearErrors(() => UserName);

      try
      {
        IPAddress.Parse(IpAddress);
      }
      catch (FormatException)
      {
        _errorsContainer.SetErrors(() => IpAddress, new[] { "IP address not is parsing" });
      }

      if (Port < IPEndPoint.MinPort || Port > IPEndPoint.MaxPort) {
        _errorsContainer.SetErrors(() => Port, new[] { "Port not available" });
      }

      if (UserName?.Length == 0)
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "User not entered" });
      }

      if (UserName?.Length > 16)
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "Username up to 16 characters" });
      }

      //if (!new Regex(UserSettings.USERNAME_MASK, RegexOptions.IgnoreCase).IsMatch(UserName)) {
      //  _errorsContainer.SetErrors(
      //    () => UserName,
      //    new[] { string.Format(Resources.UserNameUnacceptableSymbols, UserSettings.USERNAME_UNACCEPTABLE_SYMBOLS) });
      //}

      IsAvailableRightButton = _errorsContainer.GetErrors().Count == 0;
    }

    private void ExecuteSendCommandTest()
    {
      IpAddress = "192.168.37.228";
      Port = 3000;
      SelectTypeInterface = InterfaceType.WebSocet.ToString();
      UserName = "User1";
    }

    private void IsAvailable()
    {
      IsAvailableRightButton = true;
    }

    #endregion
  }
}
