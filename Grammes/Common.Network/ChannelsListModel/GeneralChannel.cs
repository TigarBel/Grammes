namespace Common.Network.ChannelsListModel
{
  using Messages.MessageReceived;

  public class GeneralChannel : BaseChannel
  {
    #region Constructors

    public GeneralChannel()
      : base("General", ChannelType.General)
    {
    }

    #endregion
  }
}
