namespace Common.DataBase.DataBase.Repository
{
  using Table;

  public class PrivateMessageRepository : Repository<PrivateMessage>
  {
    #region Constructors

    public PrivateMessageRepository(GrammesDbContext context)
      : base(context)
    {
    }

    #endregion
  }
}
