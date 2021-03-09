namespace Server.Service
{
  using System;
  using System.ServiceProcess;

  using BusinessLogic;
  using BusinessLogic.Config;

  using Common.DataBase;

  public partial class GrammesServerService : ServiceBase
  {
    #region Fields

    private NetworkManager _networkManager;

    #endregion

    #region Constructors

    public GrammesServerService()
    {
      InitializeComponent();
    }

    #endregion

    #region Methods

    protected override void OnStart(string[] args)
    {
      Config config = ServerConfig.GetDefaultConfig();
      _networkManager = new NetworkManager(
        config.WebAddress,
        config.TcpAddress,
        Convert.ToInt32(config.Timeout),
        new DataBaseManager(config.DataSource, config.Catalog));
      _networkManager.Start();
    }

    protected override void OnStop()
    {
      _networkManager.Stop();
    }

    #endregion
  }
}
