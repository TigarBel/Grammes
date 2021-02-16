namespace Client.BusinessLogic.Model.Network
{
  using System;

  using global::Common.Network;
  using global::Common.Network.Messages;

  public class NetworkController : IConnectionController
  {
    #region Fields

    public bool IsConnected;

    private WsClient _client;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<LoginEventArgs> Login;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Methods

    public void Connect(string address, int port, string name)
    {
      _client = new WsClient(name);
      _client.ConnectionStateChanged += HandleConnectionStateChanged;
      _client.LoginEvent += HandleLogin;
      _client.MessageReceived += HandleMessageReceived;
      _client.ConnectAsync(address, port);
    }

    public void Disconnect()
    {
      _client.ConnectionStateChanged -= HandleConnectionStateChanged;
      _client.LoginEvent -= HandleLogin;
      _client.MessageReceived -= HandleMessageReceived;
      _client.Disconnect();
    }

    public void Send<TClass>(BaseContainer<TClass> message)
    {
      _client.Send(message);
    }

    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      ConnectionStateChanged?.Invoke(sender, e);
    }

    private void HandleLogin(object sender, LoginEventArgs e)
    {
      Login?.Invoke(sender, e);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {
      MessageReceived?.Invoke(sender, e);
    }

    #endregion
  }
}
