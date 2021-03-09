namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using ChannelsListModel;

  using DataBaseAndNetwork.EventLog;

  public class LoginResponseContainer : BaseContainer<Response>
  {
    #region Properties

    public GeneralChannel General { get; }

    public List<PrivateChannel> OnlineList { get; }

    public List<PrivateChannel> OfflineList { get; }

    public List<EventLogMessage> EventLogMessageList { get; }

    #endregion

    #region Constructors

    public LoginResponseContainer(Response response, GeneralChannel general, List<PrivateChannel> onlineList, List<PrivateChannel> offlineList, List<EventLogMessage> eventLogMessageList)
      : base(DispatchType.Login, response)
    {
      General = general;
      OnlineList = onlineList;
      OfflineList = offlineList;
      EventLogMessageList = eventLogMessageList;
    }

    #endregion
  }
}
