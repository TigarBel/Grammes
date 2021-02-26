namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class EventRepository : Repository<Event>
  {
    #region Properties

    public GrammesDbContext GrammesDbContext => _context as GrammesDbContext;

    #endregion

    #region Constructors

    public EventRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
