namespace Common.Network.Messages
{
  public class MessageRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public MessageRequestContainer(string message)
      : base(message, DispatchType.MessageRequest)
    {
    }

    #endregion
  }
}
