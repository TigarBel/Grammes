namespace Common.Network.Messages
{
  public class MessageBroadcastContainer : BaseContainer<string>
  {
    #region Constructors

    public MessageBroadcastContainer(string message)
      : base(message, DispatchType.MessageBroadcast)
    {
    }

    #endregion
  }
}
