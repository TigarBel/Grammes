namespace Client.BusinessLogic.Model.MessagesModel
{
  using System;

  using UsersListModel;

  public class MessageModel
  {
    #region Fields

    public BaseUser Sender;

    public bool IsYou;

    public string Message;

    public DateTime Time;

    public bool IsRead;

    #endregion

    #region Constructors

    public MessageModel(OnlineUser client, BaseUser sender, string message, DateTime time, bool isRead)
    {
      Sender = sender;
      IsYou = Sender.Equals(client);
      Message = message;
      Time = time;
      IsRead = isRead;
    }

    #endregion
  }
}
