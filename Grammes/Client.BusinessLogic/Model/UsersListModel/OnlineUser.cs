namespace Client.BusinessLogic.Model.UsersListModel
{
  public class OnlineUser : BaseUser
  {
    #region Constructors

    public OnlineUser(string name)
      : base(name, UserStatus.Online)
    {
    }

    #endregion
  }
}
