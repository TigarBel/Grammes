namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public class LogEventArgs
  {
    #region Properties

    public EventLogMessage EventLog { get; }

    #endregion

    #region Constructors

    public LogEventArgs(EventLogMessage eventLog)
    {
      EventLog = eventLog;
    }

    #endregion
  }
}
