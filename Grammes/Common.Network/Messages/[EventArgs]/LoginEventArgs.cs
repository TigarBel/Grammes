namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using ChannelsListModel;
  using ChannelsListModel.BaseUserChannel;

  using EventLog;

  public class LoginEventArgs : ConnectionStateChangedEventArgs
  {
    #region Properties

    public GeneralChannel General { get; private set; }

    public List<PrivateChannel> OnlineList { get; private set; }

    public List<PrivateChannel> OfflineList { get; private set; }

    #endregion

    #region Constructors

    public LoginEventArgs(string clientName, 
                          bool connected, 
                          EventLogMessage eventLog, 
                          GeneralChannel general,
                          List<PrivateChannel> onlineList, 
                          List<PrivateChannel> offlineList)
      : base(clientName, connected, eventLog)
    {
      General = general;
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
