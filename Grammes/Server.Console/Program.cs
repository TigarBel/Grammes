namespace Server.Console
{
  using System;
  using System.Net;

  using BusinessLogic;
  using BusinessLogic.Config;

  internal class Program
  {
    #region Methods

    private static void Main(string[] args)
    {
      try
      {
        ServerConfig.SetConfig(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 64500), 1000);
        var networkManager = new NetworkManager(ServerConfig.GetConfig().Address, 25);
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
