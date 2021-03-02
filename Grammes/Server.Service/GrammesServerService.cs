namespace Server.Service
{
  using System;
  using System.Net;
  using System.ServiceProcess;

  using BusinessLogic;
  using BusinessLogic.Config;

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
      Config config = new Config(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 64500), 30);
      _networkManager = new NetworkManager(config.Address, Convert.ToInt32(config.Timeout));
      _networkManager.Start();
    }

    protected override void OnStop()
    {
      _networkManager.Stop();
    }

    #endregion
  }
}
