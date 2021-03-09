namespace Client.BusinessLogic.Model.Network
{
  using System;

  using Common.Network;
  using Common.Network.Messages;

  public class NetworkController : ICurrentConnection
  {
    #region Properties

    public InterfaceType Type { get; private set; }
    public IConnectionController ConnectionController { get; private set; }

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
      ConnectionController = null;
      Type = interfaceType;
      switch (Type)
      {
        case InterfaceType.WebSocket:
          ConnectionController = new WsNetworkController();
          break;
        case InterfaceType.Tcp:
          ConnectionController = new TcpNetworkController();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
      }

      ConnectionController.Connect(address, port, login);
      ConnectionController.ConnectionStateChanged += HandleConnectionStateChanged;
      ConnectionController.Login += HandleLogin;
      ConnectionController.MessageReceived += HandleMessageReceived;
      ConnectionController.UpdateChannel += HandleUpdateChannel;
      ConnectionController.LogEvent += HandleLog;
    }

    public void Disconnect()
    {
      ConnectionController.Disconnect();
    }

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      ConnectionController.Send(message);
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
