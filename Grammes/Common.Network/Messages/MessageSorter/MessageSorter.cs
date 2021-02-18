namespace Common.Network.Messages.MessageSorter
{
  using System;

  using EventLog;

  using MessageReceived;

  using Newtonsoft.Json.Linq;

  public class MessageSorter
  {
    #region Methods

    public static MessageReceivedEventArgs GetSortedEventMessage(JObject message)
    {
      if (message.ToObject(typeof(PrivateMessageContainer)) is PrivateMessageContainer )
      {
        var messageRequest = (PrivateMessageContainer)message.ToObject(typeof(PrivateMessageContainer));
        if (messageRequest?.Target != null)
          return new MessageReceivedEventArgs(messageRequest.Author, messageRequest.Content, 
            new PrivateAgenda(messageRequest.Target), DateTime.Now);
      }

      if (message.ToObject(typeof(GeneralMessageContainer)) is GeneralMessageContainer)
      {
        var messageRequest = (GeneralMessageContainer)message.ToObject(typeof(GeneralMessageContainer));
        if (messageRequest != null)
        {
          return new MessageReceivedEventArgs(messageRequest.Author, messageRequest.Content, new GeneralAgenda(), DateTime.Now);
        }
      }

      throw new ArgumentException("Get sorted message unknown!");
    }

    public static BaseContainer<string> GetSortedMessage(string author, string message, BaseAgenda agenda)
    {
      switch (agenda.Type)
      {
        case ChannelType.General: return new GeneralMessageContainer(author, message);
        case ChannelType.Private: return new PrivateMessageContainer(author, ((PrivateAgenda)agenda).Target, message);
        case ChannelType.Group:
          throw new ArgumentOutOfRangeException();
        default:
          throw new ArgumentOutOfRangeException();
      }

      throw new ArgumentException("Get sorted message without type!");
    }

    public static ConnectionStateChangedEventArgs GetSortedChannel(JObject message)
    {
      if (message.ToObject(typeof(ChannelResponseContainer)) is ChannelResponseContainer messageResponse)
      {
        return new ConnectionStateChangedEventArgs(
          messageResponse.Content.Login,
          messageResponse.Content.IsConnect,
          new EventLogMessage("server", true, DispatchType.Channel, "Update channel", DateTime.Now));
      }

      throw new ArgumentException("Get sorted message without type!");
    }

    #endregion
  }
}
