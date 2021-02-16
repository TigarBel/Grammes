namespace Common.Network.Messages
{
  using System;

  using MessageReceived;

  using Newtonsoft.Json.Linq;

  public class MessageSorter
  {
    #region Methods

    public static MessageReceivedEventArgs GetSortedMessage(string author,JObject message)
    {
      if (message.ToObject(typeof(GeneralMessageRequestContainer)) is GeneralMessageRequestContainer messageRequest)
      {
        return new MessageReceivedEventArgs(author, messageRequest.Content,
          new GeneralMessageReceived(), messageRequest.TimePoint);
      }

      throw new ArgumentException("Get sorted message unknown!");
    }

    #endregion
  }
}
