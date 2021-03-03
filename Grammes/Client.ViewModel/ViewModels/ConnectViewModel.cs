namespace Client.ViewModel.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Text.RegularExpressions;

  using Common.Network;

  using EventAggregator;

  using Leaf;

  using Prism.Events;

  using Properties;

  public class ConnectViewModel : LeafViewModel
  {
    #region Fields

    private string _warning;

    private string _ipAddress;

    private int _port;

    private InterfaceType _selectTypeInterface;

    private List<InterfaceType> _typeInterfaceList = new List<InterfaceType>
    {
      InterfaceType.WebSocket,
      InterfaceType.TcpSocket
    };

    private string _loginName;

    private readonly IEventAggregator _userNameEa;

    private string _ipTollTip;

    private string _addressTollTip;

    private string _userTollTip;

    #endregion

    #region Properties

    public string Warning
    {
      get => _warning;
      set => SetProperty(ref _warning, value);
    }

    public List<InterfaceType> TypeInterfaceList
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

    public InterfaceType SelectTypeInterface
    {
      get => _selectTypeInterface;
      set => SetProperty(ref _selectTypeInterface, value);
    }

    public string LoginName
    {
      get => _loginName;
      set
      {
        SetProperty(ref _loginName, value);
        Check();
        _userNameEa.GetEvent<LoginNameEvent>().Publish(_loginName);
      }
    }

    public string IpTollTip
    {
      get => _ipTollTip;
      set => SetProperty(ref _ipTollTip, value);
    }

    public string AddressTollTip
    {
      get => _addressTollTip;
      set => SetProperty(ref _addressTollTip, value);
    }

    public string UserTollTip
    {
      get => _userTollTip;
      set => SetProperty(ref _userTollTip, value);
    }

    #endregion

    #region Constructors

    public ConnectViewModel(IEventAggregator eventAggregator)
      : base("Connect")
    {
      Warning = "";
      _userNameEa = eventAggregator;
      IpAddress = "127.0.0.1";
      Port = 64500;
      LoginName = "";
      SelectTypeInterface = InterfaceType.WebSocket;
    }

    #endregion

    #region Methods

    public override void Check()
    {
      IpTollTip = "Accepted";
      AddressTollTip = "Accepted";
      UserTollTip = "Accepted";

      _errorsContainer.ClearErrors(() => IpAddress);

      if (!new Regex(Resources.IpAddressUnacceptableSymbols, RegexOptions.IgnoreCase).IsMatch(IpAddress ?? string.Empty))
      {
        _errorsContainer.SetErrors(() => IpAddress, new[] { "IP address not is parsing [127.0.0.1]" });
      }

      _errorsContainer.ClearErrors(() => Port);

      if (Port < IPEndPoint.MinPort || Port > IPEndPoint.MaxPort)
      {
        _errorsContainer.SetErrors(() => Port, new[] { "Port not available" });
      }

      _errorsContainer.ClearErrors(() => LoginName);

      if (LoginName?.Length > 16)
      {
        _errorsContainer.SetErrors(() => LoginName, new[] { "Username up to 16 characters" });
      }

      if (!new Regex(Resources.UserNameUnacceptableSymbols, RegexOptions.IgnoreCase).IsMatch(LoginName ?? string.Empty))
      {
        _errorsContainer.SetErrors(() => LoginName, new[] { "Username unmasked [User1]" });
      }

      if (_errorsContainer.GetErrors().Any(k => k.Key == "IpAddress"))
      {
        IpTollTip = _errorsContainer.GetErrors().FirstOrDefault(k => k.Key == "IpAddress").Value[0];
      }

      if (_errorsContainer.GetErrors().Any(k => k.Key == "Port"))
      {
        AddressTollTip = _errorsContainer.GetErrors().FirstOrDefault(k => k.Key == "Port").Value[0];
      }

      if (_errorsContainer.GetErrors().Any(k => k.Key == "LoginName"))
      {
        UserTollTip = _errorsContainer.GetErrors().FirstOrDefault(k => k.Key == "LoginName").Value[0];
      }

      IsAvailableButton = _errorsContainer.GetErrors().Count == 0;
    }

    private void IsAvailable()
    {
      IsAvailableButton = true;
    }

    #endregion
  }
}
