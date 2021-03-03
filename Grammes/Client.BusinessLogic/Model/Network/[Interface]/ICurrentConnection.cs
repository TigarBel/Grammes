namespace Client.BusinessLogic.Model.Network
{
  using System;

  using Common.Network;
  using Common.Network.Messages;

  public interface ICurrentConnection //Hello from C# 8.0 : IConnectionController
  {
    #region Properties

    IConnectionController ConnectionController { get; }

    #endregion

    #region Events

    event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged; //Hello from C# 8.0
    event EventHandler<LoginEventArgs> Login; //Hello from C# 8.0
    event EventHandler<MessageReceivedEventArgs> MessageReceived; //Hello from C# 8.0
    event EventHandler<UpdateChannelEventArgs> UpdateChannel; //Hello from C# 8.0
    event EventHandler<LogEventArgs> LogEvent; //Hello from C# 8.0

    #endregion

    #region Methods

    void Connect(string address, int port, string login, InterfaceType interfaceType);

    void Disconnect(); //Hello from C# 8.0

    void Send<TClass>(BaseContainer<TClass> message); //Hello from C# 8.0

    #endregion

    //Hello from C# 8.0 void Connect(string address, int port, string login);
  }
}
