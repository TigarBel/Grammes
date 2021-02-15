namespace Server.BusinessLogic.Config
{
  using System;
  using System.IO;
  using System.Net;

  using BusinessLogic.ServerConfig.Address;

  using Newtonsoft.Json;

  public class ServerConfig
  {
    #region Methods

    public void SetAddress(IPEndPoint address)
    {
      try
      {
        File.WriteAllText("server.conf.json", JsonConvert.SerializeObject(address, GetSettings()));
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    public IPEndPoint GetAddress()
    {
      try
      {
        return JsonConvert.DeserializeObject<IPEndPoint>(File.ReadAllText("server.conf.json"), GetSettings());
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    private JsonSerializerSettings GetSettings()
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
