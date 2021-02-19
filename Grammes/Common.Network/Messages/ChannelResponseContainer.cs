namespace Common.Network.Messages
{
  using Channels;

  public class ChannelResponseContainer : BaseContainer<UpdateChannel>
  {
    #region Constructors

    public ChannelResponseContainer(UpdateChannel channel, string author)
      : base(DispatchType.Channel, channel)
    {
      Author = author;
    }

    #endregion
  }
}
