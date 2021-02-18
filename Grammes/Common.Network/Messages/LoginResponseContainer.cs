namespace Common.Network.Messages
{
  using System.Collections.Generic;

  public class LoginResponseContainer : BaseContainer<Response>
  {
    #region Properties

    public List<string> OnlineList { get; }

    public List<string> OfflineList { get; }

    #endregion

    #region Constructors

    public LoginResponseContainer(Response response, List<string> onlineList, List<string> offlineList)
      : base(DispatchType.Login, response)
    {
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
