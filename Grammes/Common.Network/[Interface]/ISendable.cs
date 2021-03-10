namespace Common.Network
{
  using Messages;
  using Messages.MessageReceived;

  public interface ISendable
  {
    #region Methods

    void Send<TContainer>(BaseContainer<TContainer> message, BaseAgenda agenda);

    #endregion
  }
}
