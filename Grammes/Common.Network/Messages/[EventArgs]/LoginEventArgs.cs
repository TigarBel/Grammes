namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using ChannelsListModel;

  using DataBaseAndNetwork.EventLog;

  public class LoginEventArgs : ConnectionStateChangedEventArgs
  {
    #region Properties

    public GeneralChannel General { get; }

    public List<PrivateChannel> OnlineList { get; }

    public List<PrivateChannel> OfflineList { get; }
    
    public List<EventLogMessage> EventLogMessages { get; }

    #endregion

    #region Constructors

    public LoginEventArgs(
      string clientName,
      bool connected,
      EventLogMessage eventLog,
      GeneralChannel general,
      List<PrivateChannel> onlineList,
      List<PrivateChannel> offlineList,
      List<EventLogMessage> eventLogMessages)
      : base(clientName, connected, eventLog)
    {
      General = general;
      OnlineList = onlineList;
      OfflineList = offlineList;
      EventLogMessages = eventLogMessages;
    }

    #endregion
  }
}
