namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public class PrivateMessageContainer : BaseContainer<string>
  {
    #region Properties

    public string Target { get; }

    #endregion

    #region Constructors

    public PrivateMessageContainer(string author, string target, string message)
      : base(DispatchType.Message, message)
    {
      Author = author;
      Target = target;
    }

    #endregion
  }
}
