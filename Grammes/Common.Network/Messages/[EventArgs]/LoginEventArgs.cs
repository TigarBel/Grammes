namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using EventLog;

  public class LoginEventArgs : ConnectionStateChangedEventArgs
  {
    #region Properties

    public List<string> OnlineList { get; }

    public List<string> OfflineList { get; }

    #endregion

    #region Constructors

    public LoginEventArgs(string clientName, bool connected, EventLogMessage eventLog,
                          List<string> onlineList, List<string> offlineList)
      : base(clientName, connected, eventLog)
    {
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
