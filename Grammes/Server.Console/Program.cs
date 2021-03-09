namespace Server.Console
{
  using System;
  using System.Net;

  using BusinessLogic;
  using BusinessLogic.Config;

  using Common.DataBase;

  internal class Program
  {
    #region Methods

    private static void Main(string[] args)
    {
      try
      {
        Config config = ServerConfig.GetDefaultConfig();
        var networkManager = new NetworkManager(
          config.WebAddress,
          config.TcpAddress,
          Convert.ToInt32(config.Timeout),
          new DataBaseManager(config.DataSource, config.Catalog));
        networkManager.Start();

        Console.ReadKey(true);
        networkManager.Stop();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    #endregion
  }
}
