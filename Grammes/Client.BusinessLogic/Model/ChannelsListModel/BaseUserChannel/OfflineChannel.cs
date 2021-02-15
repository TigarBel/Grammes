namespace Client.BusinessLogic.Model.ChannelsListModel.BaseUserChannel
{
  public class OfflineChannel : UserChannel
  {
    #region Constructors

    public OfflineChannel(string name)
      : base(name, false)
    {
    }

    #endregion
  }
}
