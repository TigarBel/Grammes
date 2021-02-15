namespace Common.Network.Messages
{
  using EventLog;

  public class ConnectionStateChangedEventArgs
  {
    #region Properties

    public string ClientName { get; }

    public bool Connected { get; }

    public EventLogMessage EventLog { get; }

    #endregion

    #region Constructors

    public ConnectionStateChangedEventArgs(string clientName, bool connected, EventLogMessage eventLog)
    {
      ClientName = clientName;
      Connected = connected;
      EventLog = eventLog;
    }

    #endregion
  }
}
