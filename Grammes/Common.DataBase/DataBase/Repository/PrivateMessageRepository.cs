namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class PrivateMessageRepository : Repository<PrivateMessage>
  {
    #region Properties

    public GrammesDbContext GrammesDbContext => _context as GrammesDbContext;

    #endregion

    #region Constructors

    public PrivateMessageRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
