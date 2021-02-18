namespace Common.Network.Messages
{
  using System.Collections.Generic;

  public class ChannelUsersListEventArgs
  {
    #region Fields

    public List<string> OnlineList;

    public List<string> OfflineList;

    #endregion

    #region Constructors

    public ChannelUsersListEventArgs(List<string> onlineList, List<string> offlineList)
    {
      OnlineList = onlineList;
      OfflineList = offlineList;
    }

    #endregion
  }
}
