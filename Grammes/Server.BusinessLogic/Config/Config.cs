namespace Server.BusinessLogic.Config
{
  using System.Net;

  public class Config
  {
    #region Properties

    public IPEndPoint WebAddress { get; }

    public IPEndPoint TcpAddress { get; }

    public uint Timeout { get; }

    public string DataSource { get; }

    public string Catalog { get; }

    #endregion

    #region Constructors

    public Config(IPEndPoint webAddress, IPEndPoint tcpAddress, uint timeOut, string dataSource, string catalog)
    {
      WebAddress = webAddress;
      TcpAddress = tcpAddress;
      Timeout = timeOut;
      DataSource = dataSource;
      Catalog = catalog;
    }

    #endregion
  }
}
