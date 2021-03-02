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
        ServerConfig.SetConfig(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 64500), 10, null, null);
        Config config = ServerConfig.GetConfig();
        var networkManager = new NetworkManager(config.Address, Convert.ToInt32(config.Timeout),
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
