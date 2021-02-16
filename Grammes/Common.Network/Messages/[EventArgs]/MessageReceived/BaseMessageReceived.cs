namespace Common.Network.Messages.MessageReceived
{
  public abstract class BaseMessageReceived
  {
    #region Properties

    public MessageReceivedType Type { get; }

    #endregion

    #region Constructors

    protected BaseMessageReceived(MessageReceivedType type)
    {
      Type = type;
    }

    #endregion
  }
}
