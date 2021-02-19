namespace Client.BusinessLogic.Model.Network
{
  using System;

  using global::Common.Network;
  using global::Common.Network.Messages;

  public class NetworkController : IConnectionController
  {
    #region Fields

    private WsClient _client;

    #endregion

    #region Events

    public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    public event EventHandler<LoginEventArgs> Login;
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    public event EventHandler<UpdateChannelEventArgs> UpdateChannel;

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
      }

      _client = new WsClient(name);
      _client.ConnectionStateChanged += HandleConnectionStateChanged;
      _client.LoginEvent += HandleLogin;
      _client.MessageReceived += HandleMessageReceived;
      _client.UpdateChannel += HandleUpdateChannel;
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

    #endregion
  }
}
