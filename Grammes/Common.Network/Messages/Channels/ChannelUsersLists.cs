namespace Common.Network.Messages.Channels
{
  using System.Collections.Generic;

  public class ChannelUsersLists
  {
    #region Fields

    public List<string> OnlineList;

    public List<string> OfflineList;

    #endregion

    #region Constructors

    public ChannelUsersLists(List<string> onlineList, List<string> offlineList)
    {
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
