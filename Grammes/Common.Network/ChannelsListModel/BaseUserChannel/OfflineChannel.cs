namespace Common.Network.ChannelsListModel.BaseUserChannel
{
  public class OfflineChannel : PrivateChannel
  {
    #region Constructors

    public OfflineChannel(string name)
      : base(name, false)
    {
    }

    #endregion
  }
}
