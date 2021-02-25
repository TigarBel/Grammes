namespace Common.Network.Messages
{
  using System.Collections.Generic;

  using ChannelsListModel;

  public class LoginResponseContainer : BaseContainer<Response>
  {
    #region Properties

    public GeneralChannel General { get; private set; }

    public List<PrivateChannel> OnlineList { get; private set; }

    public List<PrivateChannel> OfflineList { get; private set; }

    #endregion

    #region Constructors

    public LoginResponseContainer(Response response, 
                                  GeneralChannel general, 
                                  List<PrivateChannel> onlineList, 
                                  List<PrivateChannel> offlineList)
      : base(DispatchType.Login, response)
    {
      General = general;
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
