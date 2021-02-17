namespace Common.Network.Messages.MessageSorter
{
  using System;

  using MessageReceived;

  using Newtonsoft.Json.Linq;

  public class MessageSorter
  {
    #region Methods

    public static MessageReceivedEventArgs GetSortedMessage(JObject message)
    {
      if (message.ToObject(typeof(GeneralMessageContainer)) is GeneralMessageContainer messageRequest) 
      {
        return new MessageReceivedEventArgs(messageRequest.Author, messageRequest.Content,
          new GeneralAgenda(), DateTime.Now);
      }

      throw new ArgumentException("Get sorted message unknown!");
    }

    #endregion
  }
}
