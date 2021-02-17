namespace Common.Network.Messages.MessageReceived
{
  public abstract class BaseAgenda
  {
    #region Properties

    public ChannelType Type { get; }

    #endregion

    #region Constructors

    protected BaseAgenda(ChannelType type)
    {
      Type = type;
    }

    #endregion
  }
}
