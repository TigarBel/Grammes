namespace Server.Service
{
  using System.ComponentModel;
  using System.Configuration.Install;
  using System.ServiceProcess;

  [RunInstaller(true)]
  public partial class InstallerGrammesServer : Installer
  {
    ServiceInstaller serviceInstaller;
    ServiceProcessInstaller processInstaller;
    #region Constructors

    public InstallerGrammesServer()
    {
      InitializeComponent();
      serviceInstaller = new ServiceInstaller();
      processInstaller = new ServiceProcessInstaller();

      processInstaller.Account = ServiceAccount.LocalSystem;
      serviceInstaller.StartType = ServiceStartMode.Manual;
      serviceInstaller.ServiceName = "GrammesServerService";
      serviceInstaller.DisplayName = "Grammes Service";
      Installers.Add(processInstaller);
      Installers.Add(serviceInstaller);
    }

    #endregion
  }
}
