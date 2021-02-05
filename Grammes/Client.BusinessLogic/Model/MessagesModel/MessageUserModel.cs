namespace Client.BusinessLogic.Model.MessagesModel
{
  using System;

  using UsersListModel;

  public class MessageUserModel
  {
    #region Properties

    public BaseUser Sender { get; set; }

    public bool IsNotClient { get; set; }

    public string Message { get; set; }

    public string Time { get; set; }

    public bool IsRead { get; set; }

    #endregion

    #region Constructors

    public MessageUserModel(OnlineUser client, BaseUser sender, string message, DateTime time, bool isRead)
    {
      Sender = sender;
      IsNotClient = !Sender.Equals(client);
      Message = message;
      Time = time.ToString("hh:mm:ss");
      IsRead = isRead;
    }

    #endregion
  }
}
