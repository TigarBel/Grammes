namespace Server.BusinessLogic.Config
{
  using System;
  using System.IO;
  using System.Net;

  using BusinessLogic.ServerConfig.Address;

  using Newtonsoft.Json;

  public static class ServerConfig
  {
    #region Methods

    public static void SetConfig(IPEndPoint webAddress, IPEndPoint tcpAddress, uint timeOut, string dataSource, string catalog)
    {
      try
      {
        var config = new Config(webAddress, tcpAddress, timeOut, dataSource, catalog);
        File.WriteAllText("server.conf.json", JsonConvert.SerializeObject(config, GetSettings()));
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    public static Config GetConfig()
    {
      try
      {
        return JsonConvert.DeserializeObject<Config>(File.ReadAllText("server.conf.json"), GetSettings());
      }
      catch
      {
        return GetDefaultConfig();
      }
    }

    public static Config GetDefaultConfig()
    {
      var webAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 64500);
      var tcpAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 64505);
      uint timeOut = 100;
      string dataSource = @"(localdb)\MSSQLLocalDB";
      string catalog = "GrammesDb";
      var config = new Config(webAddress, tcpAddress, timeOut, dataSource, catalog);
      File.WriteAllText("server.conf.json", JsonConvert.SerializeObject(config, GetSettings()));
      return JsonConvert.DeserializeObject<Config>(File.ReadAllText("server.conf.json"), GetSettings());
    }

    private static JsonSerializerSettings GetSettings()
    {
      var settings = new JsonSerializerSettings();
      settings.Converters.Add(new IPAddressConverter());
      settings.Converters.Add(new IPEndPointConverter());
      settings.Formatting = Formatting.Indented;
      return settings;
    }

    #endregion
  }
}
