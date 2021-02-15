using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic.Model.Network
{
  using System.Net;

  using global::Common.Network;
  using global::Common.Network.Messages;

  public class NetworkClient
  {
    private readonly WsClient _client;

    public bool IsConnected;

    public NetworkClient(string name, IPEndPoint ipEndPoint)
    {
      _client = new WsClient(name);
      _client.ConnectionStateChanged += HandleConnectionStateChanged;
      _client.MessageReceived += HandleMessageReceived;
      _client.ConnectAsync(ipEndPoint.Address.ToString(), ipEndPoint.Port);
    }

    public void Send(MessageRequestContainer message)
    {
      _client.Send(message);
    }

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {

    }


    private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
    {
      IsConnected = e.Connected;
    }

    public void LostConnection()
    {
      _client.ConnectionStateChanged -= HandleConnectionStateChanged;
      _client.MessageReceived -= HandleMessageReceived;
      _client.Disconnect();
    }
  }
}
