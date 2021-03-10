namespace Common.Network
{
  using System;

  using Messages;
  using Messages.MessageReceived;

  public interface IServerable
  {
    #region Events

    event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
    event EventHandler<MessageReceivedEventArgs> MessageReceived;

    #endregion

    #region Methods

    void Send<TClass>(BaseContainer<TClass> message, BaseAgenda agenda);

    bool IsContains(string user);

    #endregion
  }
}
