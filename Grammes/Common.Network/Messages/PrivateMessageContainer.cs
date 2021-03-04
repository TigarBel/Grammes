namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public class PrivateMessageContainer : BaseContainer<string>, IInterfacable
  {
    #region Properties

    public string Target { get; }
    public InterfaceType Type { get; }

    #endregion

    #region Constructors

    public PrivateMessageContainer(string author, string target, string message, InterfaceType type)
      : base(DispatchType.Message, message)
    {
      Author = author;
      Target = target;
      Type = type;
    }

    #endregion
  }
}
