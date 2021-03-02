namespace Server.Service
{
  using System.ServiceProcess;

  internal static class Program
  {
    #region Methods

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    private static void Main()
    {
      ServiceBase[] ServicesToRun;
      ServicesToRun = new ServiceBase[] { new GrammesServerService() };
      ServiceBase.Run(ServicesToRun);
    }

    #endregion
  }
}
