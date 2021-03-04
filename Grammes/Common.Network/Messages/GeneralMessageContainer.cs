namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public class GeneralMessageContainer : BaseContainer<string>, IInterfacable
  {
    #region Properties

    public InterfaceType Type { get; }

    #endregion

    #region Constructors

    public GeneralMessageContainer(string author, string message, InterfaceType type)
      : base(DispatchType.Message, message)
    {
      Author = author;
      Type = type;
    }

    #endregion
  }
}
