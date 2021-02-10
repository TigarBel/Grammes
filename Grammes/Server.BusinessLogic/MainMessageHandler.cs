using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server.BusinessLogic
{
  public class MainMessageHandler : WebSocketBehavior
  {
    protected override void OnClose(CloseEventArgs e)
    {
      base.OnClose(e);

      Console.WriteLine($"{e}The connection was closed.");
    }

    protected override void OnError(ErrorEventArgs e)
    {
      base.OnError(e);

      Console.WriteLine($"Server error. Message: {e.Message}");
    }

    protected override void OnOpen()
    {
      base.OnOpen();

      Console.WriteLine("The connection to main server was open.");
    }

    protected override void OnMessage(MessageEventArgs args)
    {
      if (string.IsNullOrEmpty(args.Data))
        return;
      Console.WriteLine($"Message: {args.Data}");

      Send($"{args.Data} : Server Ok");
    }
  }
}
