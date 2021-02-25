namespace Common.Network.ChannelsListModel.BaseUserChannel
{
  public class OnlineChannel : PrivateChannel
  {
    #region Constructors

    public OnlineChannel(string name)
      : base(name, true)
    {
    }

    #endregion
  }
}
