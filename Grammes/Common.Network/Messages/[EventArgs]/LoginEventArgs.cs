namespace Common.Network.Messages
{
  using EventLog;

  public class LoginEventArgs : ConnectionStateChangedEventArgs
  {
    public LoginEventArgs(string clientName, bool connected, EventLogMessage eventLog)
      : base(clientName, connected, eventLog)
    {

    }
  }
}
