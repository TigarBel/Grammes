namespace Client.BusinessLogic.Model.Network
{
  using System;

  using global::Common.Network.Messages;

  public interface IConnectionController
  {
    #region Events

    event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    event EventHandler<LoginEventArgs> Login;
    event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion Events

    #region Methods

    void Connect(string address, int port, string login);

    void Disconnect();

    void Send<TClass>(BaseContainer<TClass> message);

    #endregion Methods
  }
}
