namespace Common.Network.Messages
{
  using Channels;

  using DataBaseAndNetwork.EventLog;

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
