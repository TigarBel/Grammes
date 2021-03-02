namespace Server.BusinessLogic.Config
{
  using System.Net;

  public class Config
  {
    #region Properties

    public IPEndPoint Address { get; }

    public uint Timeout { get; }

    public string DataSource { get; }

    public string Catalog { get; }

    #endregion

    #region Constructors

    public Config(IPEndPoint address, uint timeOut, string dataSource = @"(localdb)\MSSQLLocalDB", string catalog = "GrammesDb")
    {
      Address = address;
      Timeout = timeOut;
      DataSource = dataSource;
      Catalog = catalog;
    }

    #endregion
  }
}
