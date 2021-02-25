namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using ChannelsListModel;
  using ChannelsListModel.BaseUserChannel;

  using DataBase;

  public class LoginResponseContainer : BaseContainer<Response>
  {
    private Response response;
    private GeneralChannel general;
    private List<OnlineChannel> onlineList;
    private List<OfflineChannel> offlineList;
    #region Properties

    public GeneralChannel General { get; }

    public List<OnlineChannel> OnlineList { get; }

    public List<OfflineChannel> OfflineList { get; }

    #endregion

    #region Constructors

    public LoginResponseContainer(Response response,
                                  GeneralChannel general,
                                  List<OnlineChannel> onlineList, 
                                  List<OfflineChannel> offlineList)
      : base(DispatchType.Login, response)
    {
      General = general;
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
