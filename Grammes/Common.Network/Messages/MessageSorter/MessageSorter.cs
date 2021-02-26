namespace Common.Network.Messages.MessageSorter
{
  using System;

  using DataBaseAndNetwork.EventLog;

  using MessageReceived;

  using Newtonsoft.Json.Linq;

  public class MessageSorter
  {
    #region Methods

    public static MessageReceivedEventArgs GetSortedMessage(JObject message)
    {
      if (message.ToObject(typeof(PrivateMessageContainer)) is PrivateMessageContainer)
      {
        var messageRequest = (PrivateMessageContainer)message.ToObject(typeof(PrivateMessageContainer));
        if (messageRequest?.Target != null)
        {
          return new MessageReceivedEventArgs(messageRequest.Author, messageRequest.Content, new PrivateAgenda(messageRequest.Target), DateTime.Now);
        }
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
        default:
          throw new ArgumentOutOfRangeException("Get sorted message without type!");
      }
    }

    public static UpdateChannelEventArgs GetSortedChannel(JObject message)
    {
      if (!(message.ToObject(typeof(ChannelResponseContainer)) is ChannelResponseContainer messageResponse))
      {
        throw new ArgumentException("Get sorted channel out of range!");
      }

      string text = messageResponse.Content.IsConnect ? $"{messageResponse.Author} connect" : $"{messageResponse.Author} disconnect";
      return new UpdateChannelEventArgs(
        messageResponse.Content.Login,
        messageResponse.Content.IsConnect,
        new EventLogMessage(messageResponse.Author, true, DispatchType.Channel, text, DateTime.Now),
        messageResponse.Content.IsRegistration);
    }

    public static LogEventArgs GetSortedEventMessage(JObject message)
    {
      if (!(message.ToObject(typeof(MessageEventLogContainer)) is MessageEventLogContainer messageResponse))
      {
        throw new ArgumentException("Event log message don't sorted!");
      }

      string sender = message.Last.First.First.First.Value<string>();

      return new LogEventArgs(
        new EventLogMessage(
          sender,
          messageResponse.Content.IsSuccessfully,
          messageResponse.Content.Type,
          messageResponse.Content.Text,
          messageResponse.Content.Time));
    }

    #endregion
  }
}
/*
      if (message.ToObject(typeof(MessageEventLogContainer)) is MessageEventLogContainer messageResponse) {
        string sender = message.Last.First.First.First.Value<string>();

        return new LogEventArgs(
          new EventLogMessage(
            sender,
            messageResponse.Content.IsSuccessfully,
            messageResponse.Content.Type,
            messageResponse.Content.Text,
            messageResponse.Content.Time));
      }*/
