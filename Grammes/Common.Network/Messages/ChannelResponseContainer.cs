namespace Common.Network.Messages
{
  using Channels;

  public class ChannelResponseContainer : BaseContainer<UpdateChannel>
  {
    #region Constructors

    public ChannelResponseContainer(UpdateChannel channel)
      : base(DispatchType.Channel, channel)
    {

    }

    #endregion
  }
}
