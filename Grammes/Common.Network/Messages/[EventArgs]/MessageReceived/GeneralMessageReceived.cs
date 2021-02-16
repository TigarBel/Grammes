namespace Common.Network.Messages.MessageReceived
{
  public class GeneralMessageReceived : BaseMessageReceived
  {
    #region Constructors

    public GeneralMessageReceived()
      : base(MessageReceivedType.General)
    {
    }

    #endregion
  }
}
