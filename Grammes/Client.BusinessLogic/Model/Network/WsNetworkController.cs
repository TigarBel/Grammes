namespace Client.BusinessLogic.Model.Network
{
  using System;

  using Common.Network;
  using Common.Network.Messages;

  public class WsNetworkController : BaseController<WsClient>, IConnectionController
  {
    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<LoginEventArgs> Login;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    public event EventHandler<UpdateChannelEventArgs> UpdateChannel;
    public event EventHandler<LogEventArgs> LogEvent;

    #endregion

    #region Constructors

    public WsNetworkController()
      : base(InterfaceType.WebSocket)
    {
    }

    #endregion

    #region Methods

    public void Connect(string address, int port, string name)
    {
      if (_client != null)
      {
        _client.ConnectionStateChanged -= HandleConnectionStateChanged;
        _client.LoginEvent -= HandleLogin;
        _client.MessageReceived -= HandleMessageReceived;
        _client.UpdateChannel -= HandleUpdateChannel;
        _client.LogEvent -= HandleLog;
      }

      _client = new WsClient(name);
      _client.ConnectionStateChanged += HandleConnectionStateChanged;
      _client.LoginEvent += HandleLogin;
      _client.MessageReceived += HandleMessageReceived;
      _client.UpdateChannel += HandleUpdateChannel;
      _client.LogEvent += HandleLog;

      _client.ConnectAsync(address, port);
    }

    public void Disconnect()
    {
      _client.Disconnect();
    }

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      _client.Send(message);
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs eventArgs)
    {
      ConnectionStateChanged?.Invoke(sender, eventArgs);
    }

    private void HandleLogin(object sender, LoginEventArgs eventArgs)
    {
      Login?.Invoke(sender, eventArgs);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs eventArgs)
    {
      MessageReceived?.Invoke(sender, eventArgs);
    }

    private void HandleUpdateChannel(object sender, UpdateChannelEventArgs eventArgs)
    {
      UpdateChannel?.Invoke(sender, eventArgs);
    }

    private void HandleLog(object sender, LogEventArgs eventArgs)
    {
      LogEvent?.Invoke(sender, eventArgs);
    }

    #endregion
  }
}
