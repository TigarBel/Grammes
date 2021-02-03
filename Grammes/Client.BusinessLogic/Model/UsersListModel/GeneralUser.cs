namespace Client.BusinessLogic.Model.UsersListModel
{
  public class GeneralUser : BaseUser
  {
    #region Constructors

    public GeneralUser()
      : base("General", UserStatus.General)
    {
    }

    #endregion
  }
}
