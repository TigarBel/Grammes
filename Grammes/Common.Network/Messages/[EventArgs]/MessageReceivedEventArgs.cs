namespace Common.Network.Messages
{
  using System;

  using MessageReceived;

  public class MessageReceivedEventArgs
  {
    #region Properties

    public string Author { get; }

    public string Message { get; }

    public BaseMessageReceived Target { get; }

    public DateTime Time { get; }

    #endregion

    #region Constructors

    public MessageReceivedEventArgs(string author, string message, BaseMessageReceived target, DateTime time)
    {
      Author = author;
      Message = message;
      Target = target;
      Time = time;
    }

    #endregion
  }
}
