namespace Server.Console
{
  using System;
  using System.IO;
  using System.Net;

  using BusinessLogic;
  using BusinessLogic.Address;
  using Newtonsoft.Json;
  using WebSocketSharp.Server;

  internal class Program
  {
    #region Methods

    private static void Main(string[] args)
    {
      IPEndPoint address = Deserialize();
      var socketServer = new WebSocketServer($"ws://{address.Address}:{address.Port}");

      socketServer.AddWebSocketService<MainMessageHandler>("/User1");

      socketServer.Start();


      if (socketServer.IsListening) {
        Console.WriteLine($"Listening on port {socketServer.Port}, and providing WebSocket services:");

        foreach (var path in socketServer.WebSocketServices.Paths) Console.WriteLine($"- {path}");
      }

      Console.WriteLine("Servers started. Press the key to exit");
      Console.ReadKey(true);
    }

    private static void Serialize(IPEndPoint address)
    {
      File.WriteAllText("server.conf.json", JsonConvert.SerializeObject(address, GetSettings()));
    }

    private static IPEndPoint Deserialize()
    {
      try {
        return JsonConvert.DeserializeObject<IPEndPoint>(File.ReadAllText("server.conf.json"), GetSettings());
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }

    private static JsonSerializerSettings GetSettings()
    {
      JsonSerializerSettings settings = new JsonSerializerSettings();
      settings.Converters.Add(new IPAddressConverter());
      settings.Converters.Add(new IPEndPointConverter());
      settings.Formatting = Formatting.Indented;
      return settings;
    }

    #endregion
  }
}
