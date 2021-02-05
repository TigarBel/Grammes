namespace Client.BusinessLogic.Model.MessagesModel
{
  using System;
  using System.Windows.Media;

  using UsersListModel;

  public class MessageUserModel
  {
    #region Fields

    #endregion

    #region Properties

    public BaseUser Sender { get; set; }

    public bool IsNotClient { get; set; }

    public string Message { get; set; }

    public string Time { get; set; }

    public bool IsRead { get; set; }

    public int GridColumn => Convert.ToInt32(IsNotClient);

    public Brush BrushMessage { get; }

    #endregion

    #region Constructors

    public MessageUserModel(OnlineUser client, BaseUser sender, string message, DateTime time, bool isRead)
    {
      Sender = sender;
      IsNotClient = !Sender.Equals(client);
      Message = message;
      Time = time.ToString("hh:mm:ss");
      IsRead = isRead;
      BrushMessage = IsRead ? Brushes.Transparent : Brushes.LightGray;
    }

    #endregion
  }
}
