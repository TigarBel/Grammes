namespace Client.BusinessLogic.Model.ChannelsListModel.BaseUserChannel
{
  public class OnlineChannel : UserChannel
  {
    #region Constructors

    public OnlineChannel(string name)
      : base(name, true)
    {
    }

    #endregion
  }
}
