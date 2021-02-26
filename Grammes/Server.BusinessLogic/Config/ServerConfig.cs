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

    public static void SetConfig(IPEndPoint address, uint timeOut)
    {
      try
      {
        Config config = new Config(address, timeOut);
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
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
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
