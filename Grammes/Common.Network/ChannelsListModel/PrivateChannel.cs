namespace Common.Network.ChannelsListModel
{
  using Messages.MessageReceived;

  public class PrivateChannel : BaseChannel
  {
    #region Fields

    public bool IsOnline { get; private set; }

    #endregion

    #region Constructors

    public PrivateChannel(string name, bool isOnline = false)
      : base(name, ChannelType.Private)
    {
      IsOnline = isOnline;
    }

    #endregion
  }
}
