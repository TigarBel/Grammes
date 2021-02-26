namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public class MessageEventLogContainer : BaseContainer<EventLogMessage>
  {
    #region Constructors

    public MessageEventLogContainer(EventLogMessage eventLogMessage)
      : base(DispatchType.EventLog, eventLogMessage)
    {
    }

    #endregion
  }
}
