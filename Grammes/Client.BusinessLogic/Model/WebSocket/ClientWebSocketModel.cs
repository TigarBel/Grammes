using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Model.WebSocket
{
  using System.Net;

  using WebSocketSharp;

  public class ClientWebSocketModel
  {
    private readonly WebSocket _client;

    public ClientWebSocketModel(string name)
    {
      _client = new WebSocket($"ws://192.168.37.228:65000/{name}");
      _client.OnMessage += Client_OnMessage;
      _client.Connect();
    }

    public void SendMessage(string message)
    {
      _client.Send(message);
    }

    private static void Client_OnMessage(object sender, MessageEventArgs e)
    {
      //Console.WriteLine(e.Data);
      //Console.WriteLine("Enter message:");
    }
  }
}
