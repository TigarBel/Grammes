namespace Common.Network.ChannelsListModel
{
  using System;

  public class MessageModel
  {
    #region Properties

    public string Message { get; set; }

    public string Time { get; set; }

    public bool IsOutgoingMessage { get; set; }

    public bool IsReadingMessage { get; set; }

    #endregion

    #region Constructors

    public MessageModel(string message, DateTime time, bool isOutgoingMessage, bool isReadingMessage)
    {
      Message = message;
      Time = time.ToString("hh:mm");
      IsOutgoingMessage = isOutgoingMessage;
      IsReadingMessage = isReadingMessage;
    }

    #endregion
  }
}
