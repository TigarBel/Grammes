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

    private string _port;

    private InterfaceType _selectTypeInterface;

    private List<InterfaceType> _typeInterfaceList = new List<InterfaceType>
    {
      InterfaceType.WebSocket,
      InterfaceType.Tcp
    };

    private string _loginName;

    private readonly IEventAggregator _userNameEa;

    private string _ipToolTip;

    private string _addressToolTip;

    private string _userToolTip;

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
      set => SetProperty(ref _ipAddress, value, Check);
    }

    public string Port
    {
      get => _port;
      set => SetProperty(ref _port, value, Check);
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
        SetProperty(ref _loginName, value, Check);
        _userNameEa.GetEvent<LoginNameEvent>().Publish(_loginName);
      }
    }

    public string IpToolTip
    {
      get => _ipToolTip;
      set => SetProperty(ref _ipToolTip, value);
    }

    public string AddressToolTip
    {
      get => _addressToolTip;
      set => SetProperty(ref _addressToolTip, value);
    }

    public string UserToolTip
    {
      get => _userToolTip;
      set => SetProperty(ref _userToolTip, value);
    }

    #endregion

    #region Constructors

    public ConnectViewModel(IEventAggregator eventAggregator)
      : base("Connect")
    {
      Warning = "";
      _userNameEa = eventAggregator;
      IpAddress = "127.0.0.1";
      Port = "64500";
      LoginName = "";
      SelectTypeInterface = InterfaceType.WebSocket;
    }

    #endregion

    #region Methods

    public override void Check()
    {
      IpToolTip = "Accepted";
      AddressToolTip = "Accepted";
      UserToolTip = "Accepted";

      _errorsContainer.ClearErrors(() => IpAddress);

      if (!new Regex(Resources.IpAddressUnacceptableSymbols, RegexOptions.IgnoreCase).IsMatch(IpAddress ?? string.Empty))
      {
        _errorsContainer.SetErrors(() => IpAddress, new[] { "IP address not is parsing [127.0.0.1]" });
      }

      _errorsContainer.ClearErrors(() => Port);

      if (int.TryParse(Port, out int result))
      {
        if (result < IPEndPoint.MinPort || result > IPEndPoint.MaxPort)
        {
          _errorsContainer.SetErrors(() => Port, new[] { "Port not available" });
        }
      }
      else
      {
        _errorsContainer.SetErrors(() => Port, new[] { "Port not integer" });
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
        IpToolTip = _errorsContainer.GetErrors().FirstOrDefault(k => k.Key == "IpAddress").Value[0];
      }

      if (_errorsContainer.GetErrors().Any(k => k.Key == "Port"))
      {
        AddressToolTip = _errorsContainer.GetErrors().FirstOrDefault(k => k.Key == "Port").Value[0];
      }

      if (_errorsContainer.GetErrors().Any(k => k.Key == "LoginName"))
      {
        UserToolTip = _errorsContainer.GetErrors().FirstOrDefault(k => k.Key == "LoginName").Value[0];
      }

      IsAvailableButton = _errorsContainer.GetErrors().Count == 0;
    }

    #endregion
  }
}
