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

  using Properties;

  public class ConnectViewModel : LeafViewModel
  {
    #region Fields

    private string _warning;

    private string _ipAddress;

    private int _port;

    private string _selectTypeInterface;

    private List<string> _typeInterfaceList = new List<string>
    {
      InterfaceType.WebSocket.ToString(),
      InterfaceType.TcpSocket.ToString()
    };

    private string _userName;

    private readonly Regex _regexLogin = new Regex(@"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$");

    private readonly IEventAggregator _userNameEa;

    #endregion

    #region Properties

    public string Warning
    {
      get => _warning;
      set => SetProperty(ref _warning, value);
    }

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
        _userNameEa.GetEvent<ChannelNameEvent>().Publish(_userName);
      }
    }

    #endregion

    #region Constructors

    public ConnectViewModel(IEventAggregator eventAggregator)
      : base("Connect")
    {
      Warning = "";
      _userNameEa = eventAggregator;
      IpAddress = "192.168.37.228";
      Port = 3000;
      UserName = "";
      SelectTypeInterface = InterfaceType.WebSocket.ToString();
    }

    #endregion

    #region Methods

    public override void Check()
    {
      _errorsContainer.ClearErrors(() => IpAddress);

      try
      {
        IPAddress.Parse(IpAddress);
      }
      catch (FormatException)
      {
        _errorsContainer.SetErrors(() => IpAddress, new[] { "IP address not is parsing" });
      }

      _errorsContainer.ClearErrors(() => Port);

      if (Port < IPEndPoint.MinPort || Port > IPEndPoint.MaxPort)
      {
        _errorsContainer.SetErrors(() => Port, new[] { "Port not available" });
      }

      _errorsContainer.ClearErrors(() => UserName);

      if (UserName?.Length == 0)
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "User not entered" });
      }

      if (UserName?.Length > 16)
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "Username up to 16 characters" });
      }

      if (!new Regex(Resources.UserNameUnacceptableSymbols, RegexOptions.IgnoreCase).IsMatch(UserName ?? string.Empty))
      {
        _errorsContainer.SetErrors(() => UserName, new[] { "Username unmasked" });
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
