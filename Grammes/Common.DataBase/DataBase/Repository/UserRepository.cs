namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class UserRepository : Repository<User>
  {
    #region Constructors

    public UserRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
