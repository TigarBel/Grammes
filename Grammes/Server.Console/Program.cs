namespace Server.Console
{
  using System;

  using BusinessLogic;

  internal class Program
  {
    #region Methods

    private static void Main(string[] args)
    {
      try
      {
        var networkManager = new NetworkManager();
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
