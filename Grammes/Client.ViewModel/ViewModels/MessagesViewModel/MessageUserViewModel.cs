namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;
  using System.Windows.Media;

  using BusinessLogic.Model.MessagesModel;
  using BusinessLogic.Model.UsersListModel;

  using Prism.Mvvm;

  public class MessageUserViewModel : BindableBase
  {
    #region Fields

    private MessageUserModel _messageUser;

    private Brush _brush;

    #endregion

    #region Properties

    public MessageUserModel MessageUser
    {
      get => _messageUser;
      set => SetProperty(ref _messageUser, value);
    }

    public Brush BrushMessage
    {
      get => _brush;
      set => SetProperty(ref _brush, value);
    }

    public int GridColumn => Convert.ToInt32(MessageUser.IsNotClient);

    #endregion

    #region Constructors

    public MessageUserViewModel(MessageUserModel message)
    {
      ///**/
      //var user1 = new OnlineUser("user1");
      //MessageUser = new MessageUserModel(user1, user1, "Hi1", new DateTime(2001, 10, 10, 10, 10, 10), true);
      ///**/
      MessageUser = message;
      BrushMessage = MessageUser.IsRead ? Brushes.Transparent : Brushes.LightGray;
    }

    #endregion
  }
}
