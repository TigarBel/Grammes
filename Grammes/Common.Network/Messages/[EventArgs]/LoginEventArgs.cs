namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using ChannelsListModel;
  using ChannelsListModel.BaseUserChannel;

  using EventLog;

  public class LoginEventArgs : ConnectionStateChangedEventArgs
  {
    #region Properties

    public GeneralChannel General { get; }

    public List<OnlineChannel> OnlineList { get; }

    public List<OfflineChannel> OfflineList { get; }

    #endregion

    #region Constructors

    public LoginEventArgs(string clientName, 
                          bool connected, 
                          EventLogMessage eventLog, 
                          GeneralChannel general,
                          List<OnlineChannel> onlineList, 
                          List<OfflineChannel> offlineList)
      : base(clientName, connected, eventLog)
    {
      General = general;
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
