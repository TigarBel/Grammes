namespace Common.Network.Messages
{
  using System;

  using MessageReceived;

  public class MessageReceivedEventArgs
  {
    #region Properties

    public string Author { get; }

    public string Message { get; }
    public InterfaceType Type { get; }

    public BaseAgenda Agenda { get; }

    public DateTime Time { get; }

    #endregion

    #region Constructors

    public MessageReceivedEventArgs(string author, string message, InterfaceType type, BaseAgenda agenda, DateTime time)
    {
      Type = type;
      Author = author;
      Message = message;
      Agenda = agenda;
      Time = time;
    }

    #endregion
  }
}
