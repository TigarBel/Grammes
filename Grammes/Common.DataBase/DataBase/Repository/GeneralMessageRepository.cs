namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class GeneralMessageRepository : Repository<GeneralMessage>
  {
    #region Properties

    public GrammesDbContext GrammesDbContext => _context as GrammesDbContext;

    #endregion

    #region Constructors

    public GeneralMessageRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
