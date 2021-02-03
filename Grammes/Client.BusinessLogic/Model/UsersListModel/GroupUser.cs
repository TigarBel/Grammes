namespace Client.BusinessLogic.Model.UsersListModel
{
  public class GroupUser : BaseUser
  {
    #region Constructors

    public GroupUser(string name)
      : base(name, UserStatus.Group)
    {
    }

    #endregion
  }
}
