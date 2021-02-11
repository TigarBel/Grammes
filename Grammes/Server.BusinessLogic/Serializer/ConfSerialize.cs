namespace Server.BusinessLogic.Serializer
{
  using System;
  using System.IO;
  using System.Net;

  using Address;

  using Newtonsoft.Json;

  public class ConfSerialize
  {
    #region Methods

    public void Serialize(IPEndPoint address)
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

    public IPEndPoint Deserialize()
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
