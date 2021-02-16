namespace Common.Network.Messages.MessageReceived
{
  using System;

  public class PrivateMessageReceived : BaseMessageReceived
  {
    #region Properties

    public string Target { get; }

    #endregion

    #region Constructors

    public PrivateMessageReceived(string target)
      : base(MessageReceivedType.Private)
    {
      Target = target ?? throw new NullReferenceException("Private message received target is null!");
    }

    #endregion
  }
}
