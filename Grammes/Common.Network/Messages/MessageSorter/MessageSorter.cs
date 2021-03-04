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
          return new MessageReceivedEventArgs(
            messageRequest.Author,
            messageRequest.Content,
            messageRequest.Type,
            new PrivateAgenda(messageRequest.Target),
            DateTime.Now);
        }
      }

      if (message.ToObject(typeof(GeneralMessageContainer)) is GeneralMessageContainer)
      {
        var messageRequest = (GeneralMessageContainer)message.ToObject(typeof(GeneralMessageContainer));
        if (messageRequest != null)
        {
          return new MessageReceivedEventArgs(messageRequest.Author, messageRequest.Content, messageRequest.Type, new GeneralAgenda(), DateTime.Now);
        }
      }

      throw new ArgumentException("Get sorted message unknown!");
    }
    public static BaseContainer<string> GetSortedMessage(MessageReceivedEventArgs eventArgs)
    {
      string author = eventArgs.Author;
      string message = eventArgs.Message;
      InterfaceType type = eventArgs.Type;
      BaseAgenda agenda = eventArgs.Agenda;
      switch (agenda.Type) {
        case ChannelType.General: return new GeneralMessageContainer(author, message, type);
        case ChannelType.Private: return new PrivateMessageContainer(author, ((PrivateAgenda)agenda).Target, message, type);
        case ChannelType.Group:
        default:
          throw new ArgumentOutOfRangeException("Get sorted message without type!");
      }
    }

    public static BaseContainer<string> GetSortedMessage(string author, string message, InterfaceType type, BaseAgenda agenda)
    {
      switch (agenda.Type)
      {
        case ChannelType.General: return new GeneralMessageContainer(author, message, type);
        case ChannelType.Private: return new PrivateMessageContainer(author, ((PrivateAgenda)agenda).Target, message, type);
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
      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = true,
        SenderName = messageResponse.Author,
        Text = text,
        Time = DateTime.Now,
        Type = DispatchType.Channel
      };

      return new UpdateChannelEventArgs(
        messageResponse.Content.Login,
        messageResponse.Content.IsConnect,
        eventLogMessage,
        messageResponse.Content.IsRegistration);
    }

    public static LogEventArgs GetSortedEventMessage(JObject message)
    {
      if (!(message.ToObject(typeof(MessageEventLogContainer)) is MessageEventLogContainer messageResponse))
      {
        throw new ArgumentException("Event log message don't sorted!");
      }

      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = messageResponse.Content.IsSuccessfully,
        SenderName = messageResponse.Content.SenderName,
        Text = messageResponse.Content.Text,
        Time = messageResponse.Content.Time,
        Type = messageResponse.Content.Type
      };

      return new LogEventArgs(eventLogMessage);
    }

    #endregion
  }
}
