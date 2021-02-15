namespace Client.BusinessLogic.Model.ChannelsListModel
{
  public abstract class UserChannel : BaseChannel
  {
    #region Fields

    public bool IsOnline { get; private set; }

    #endregion

    #region Constructors

    protected UserChannel(string name, bool isOnline)
      : base(name, ChannelType.User)
    {
      IsOnline = isOnline;
    }

    #endregion
  }
}
