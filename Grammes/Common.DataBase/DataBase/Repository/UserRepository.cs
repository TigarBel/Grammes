namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class UserRepository : Repository<User>
  {
    #region Properties

    public GrammesDbContext GrammesDbContext => _context as GrammesDbContext;

    #endregion

    #region Constructors

    public UserRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
