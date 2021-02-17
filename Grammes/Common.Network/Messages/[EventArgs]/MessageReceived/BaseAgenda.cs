namespace Common.Network.Messages.MessageReceived
{
  public abstract class BaseAgenda
  {
    #region Properties

    public MessageReceivedType Type { get; }

    #endregion

    #region Constructors

    protected BaseAgenda(MessageReceivedType type)
    {
      Type = type;
    }

    #endregion
  }
}
