namespace Common.Network.Collector
{
  using System.Collections.Generic;
  using System.Linq;

  using ChannelsListModel;
  using ChannelsListModel.BaseUserChannel;

  using DataBase.DataBase.Table;

  public static class Collector
  {
    #region Methods

    public static GeneralChannel CollectGeneralChannel(int userId, List<GeneralMessage> generalMessages)
    {
      var channel = new GeneralChannel();
      foreach (GeneralMessage message in generalMessages)
      {
        bool isOut = userId != message.User_Id;
        channel.MessageList.Add(
          isOut
            ? new MessageModel($"{message.User.Name}:{message.Message}", message.Time, isOut, true)
            : new MessageModel(message.Message, message.Time, isOut, true));
      }

      return channel;
    }

    public static List<PrivateChannel> CollectOnlineChannel(User user, List<string> onlineList, List<PrivateMessage> privateMessages)
    {
      return CollectChannel(user, onlineList, privateMessages, true);
    }

    public static List<PrivateChannel> CollectOfflineChannel(User user, List<string> offlineList, List<PrivateMessage> privateMessages)
    {
      return CollectChannel(user, offlineList, privateMessages, false);
    }

    private static List<PrivateChannel> CollectChannel(User user, List<string> userList, List<PrivateMessage> privateMessages, bool isOnline)
    {
      List<PrivateMessage> localPrivateMessages = SortPrivateMessage(userList, privateMessages)
        .Where(u => u.SenderId == user.Id || u.TargetId == user.Id).ToList();
            
      List<PrivateChannel> channelList = userList
        .Where(uName => uName != user.Name)
        .Select(channel => new PrivateChannel(channel, isOnline))
        .ToList();

      if (channelList.Count == 0) {
        return channelList;
      }

      foreach (PrivateMessage message in localPrivateMessages) {
        bool isOut = user.Id == message.TargetId;
        User target = GetTargetUser(user.Id, message);

        PrivateChannel channel = channelList.Find(u => u.Name == target.Name);
        channel.MessageList.Add(new MessageModel(message.Message, message.Time, isOut, true));
        localPrivateMessages.Remove(message);
        List<PrivateMessage> localOpm = localPrivateMessages.Where(t => t.SenderId == target.Id || t.TargetId == target.Id).ToList();
        foreach (PrivateMessage channelMessage in localOpm) {
          isOut = user.Id == channelMessage.TargetId;
          channel.MessageList.Add(new MessageModel(channelMessage.Message, channelMessage.Time, isOut, true));
          localOpm.Remove(channelMessage);
          localPrivateMessages.Remove(message);
          if (localOpm.Count == 0) {
            return channelList;
          }
        }
      }

      return channelList;
    }

    private static User GetTargetUser(int ourId, PrivateMessage message)
    {
      if (message.SenderId == ourId)
      {
        return new User
        {
          Id = message.TargetId,
          Name = message.Target.Name
        };
      }

      return new User
      {
        Id = message.SenderId,
        Name = message.Sender.Name
      };
    }

    private static List<PrivateMessage> SortPrivateMessage(List<string> list, List<PrivateMessage> privateMessages)
    {
      var messages = new List<PrivateMessage>();
      foreach (PrivateMessage message in privateMessages)
      {
        if (list.Contains(message.Sender.Name) || list.Contains(message.Target.Name))
        {
          messages.Add(message);
        }
      }

      return messages;
    }

    #endregion
  }
}
