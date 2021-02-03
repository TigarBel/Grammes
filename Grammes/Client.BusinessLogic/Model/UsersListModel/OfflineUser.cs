namespace Client.BusinessLogic.Model.UsersListModel
{
  public class OfflineUser : BaseUser
  {
    #region Constructors

    public OfflineUser(string name)
      : base(name, UserStatus.Offline)
    {
    }

    #endregion
  }
}
