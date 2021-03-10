namespace Common.Network
{
  using System;
  using System.Collections.Generic;
  using System.Net;

  using Client;

  using Messages;

  using Properties;

  public abstract class BaseServer<TGuid, TConnect, TSocket>
    where TConnect : IClosable
  {
    #region Fields

    protected readonly IPEndPoint _listenAddress;
    protected readonly Clients<TGuid, TConnect> _connections;

    protected TSocket _server;

    protected readonly string _name;

    #endregion

    #region Properties

    public List<string> UserOnlineList { get; set; }

    public List<string> UserOfflineList { get; set; }

    #endregion
    
    #region Constructors

    protected BaseServer(IPEndPoint listenAddress, int timeout)
    {
       _name = Resources.ServerName;
      _listenAddress = listenAddress;
      _connections = new Clients<TGuid, TConnect>(timeout);

      UserOfflineList = new List<string>();
      UserOnlineList = new List<string>();
    }

    #endregion
  }
}
