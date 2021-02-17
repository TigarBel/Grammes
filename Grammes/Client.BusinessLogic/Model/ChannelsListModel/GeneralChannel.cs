namespace Client.BusinessLogic.Model.ChannelsListModel
{
  using global::Common.Network.Messages.MessageReceived;

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
