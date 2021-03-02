namespace Common.DataBase.DataBase
{
  using Repository;

  public class Unit
  {
    #region Properties

    public UserRepository User { get; }

    public GeneralMessageRepository GeneralMessage { get; }

    public PrivateMessageRepository PrivateMessage { get; }

    public EventRepository Event { get; }

    #endregion

    #region Constructors

    public Unit(string dataSource, string catalog)
    {
      if (string.IsNullOrEmpty(dataSource))
      {
        dataSource = @"(localdb)\MSSQLLocalDB";
      }

      if (string.IsNullOrEmpty(catalog))
      {
        catalog = "GrammesDb";
      }

      string connectionString = $"Data Source={dataSource};Initial Catalog={catalog};Integrated Security=True;";
      var context = new GrammesDbContext(connectionString);
      User = new UserRepository(context);
      GeneralMessage = new GeneralMessageRepository(context);
      PrivateMessage = new PrivateMessageRepository(context);
      Event = new EventRepository(context);
    }

    #endregion
  }
}
