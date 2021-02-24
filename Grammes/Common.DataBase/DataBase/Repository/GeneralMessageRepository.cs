namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class GeneralMessageRepository : Repository<GeneralMessage>
  {
    #region Constructors

    public GeneralMessageRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
