namespace Common.Network
{
  using System;

  using Messages;

  public interface IConnectionController
  {
    #region Properties

    InterfaceType Type { get; }

    #endregion

    #region Events

    event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    event EventHandler<LoginEventArgs> Login;
    event EventHandler<MessageReceivedEventArgs> MessageReceived;
    event EventHandler<UpdateChannelEventArgs> UpdateChannel;
    event EventHandler<LogEventArgs> LogEvent;

    #endregion

    #region Methods

    void Connect(string address, int port, string login);

    void Disconnect();

    void Send<TClass>(BaseContainer<TClass> message);

    #endregion
  }
}
