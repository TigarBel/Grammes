namespace Client.BusinessLogic.Model.Network
{
  using System;

  using Common.Network;
  using Common.Network.Messages;

  public class NetworkController : ICurrentConnection
  {
    #region Fields

    private IConnectionController _connectionController;

    #endregion

    #region Properties

    public InterfaceType Type => _connectionController.Type;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<LoginEventArgs> Login;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    public event EventHandler<UpdateChannelEventArgs> UpdateChannel;
    public event EventHandler<LogEventArgs> LogEvent;

    #endregion

    #region Methods

    public void Connect(string address, int port, string login, InterfaceType interfaceType)
    {
      _connectionController = null;
      switch (interfaceType)
      {
        case InterfaceType.WebSocket:
          _connectionController = new WsNetworkController();
          break;
        case InterfaceType.Tcp:
          _connectionController = new TcpNetworkController();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(Type), interfaceType, null);
      }

      _connectionController.Connect(address, port, login);
      _connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
      _connectionController.Login += HandleLogin;
      _connectionController.MessageReceived += HandleMessageReceived;
      _connectionController.UpdateChannel += HandleUpdateChannel;
      _connectionController.LogEvent += HandleLog;
    }

    public void Disconnect()
    {
      _connectionController.Disconnect();
    }

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      _connectionController.Send(message);
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
