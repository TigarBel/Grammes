namespace Common.Network.Messages.MessageReceived
{
  using System;

  public class PrivateAgenda : BaseAgenda
  {
    #region Properties

    public string Target { get; }

    #endregion

    #region Constructors

    public PrivateAgenda(string target)
      : base(ChannelType.Private)
    {
      Target = target ?? throw new NullReferenceException("Private message received target is null!");
    }

    #endregion
  }
}
