namespace Client.ViewModel.ViewModels.MessagesViewModel
{
  using System;
  using System.Collections.Generic;

  using BusinessLogic.Model.MessagesModel;
  using BusinessLogic.Model.UsersListModel;

  using Prism.Mvvm;

  public class MessagesViewModel : BindableBase
  {
    #region Fields

    private List<MessageUserModel> _messagesUserList;

    #endregion

    #region Properties

    public List<MessageUserModel> MessagesUserList
    {
      get => _messagesUserList;
      set => SetProperty(ref _messagesUserList, value);
    }

    #endregion

    #region Constructors

    public MessagesViewModel(/*List<MessageUserModel> messageUserList*/)
    {
      /*Hard-Code*/
      MessagesUserList = new List<MessageUserModel>();
      OnlineUser user = new OnlineUser("User1");
      OnlineUser user2 = new OnlineUser("User2");
      MessageUserModel message = new MessageUserModel(user,user,"Oh hi!",
        new DateTime(2001,10,10,10,10,10),true);
      MessagesUserList.Add(message);
      message = new MessageUserModel(user, user2, "Oh hi!",
        new DateTime(2001, 10, 10, 10, 11, 10), true);
      MessagesUserList.Add(message);
      message = new MessageUserModel(user, user, "By-by!",
        new DateTime(2001, 10, 10, 10, 12, 10), true);
      MessagesUserList.Add(message);
      message = new MessageUserModel(user, user2, "By-by!",
        new DateTime(2001, 10, 10, 10, 13, 10), true);
      MessagesUserList.Add(message);
      message = new MessageUserModel(user, user2, "By!",
        new DateTime(2001, 10, 10, 10, 13, 10), false);
      MessagesUserList.Add(message);
      /*Hard-Code*/

      /*foreach (MessageUserModel messageUser in messageUserList)
      {
        MessagesUserList.Add(new MessageUserViewModel(messageUser));
      }*/
    }

    #endregion
  }
}
