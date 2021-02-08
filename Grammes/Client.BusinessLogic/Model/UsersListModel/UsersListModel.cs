namespace Client.BusinessLogic.Model.UsersListModel
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  public class UsersListModel
  {
    #region Properties

    public GeneralUser General { get; }

    public ObservableCollection<OnlineUser> OnlineUsers { get; set; }
    
    public ObservableCollection<OfflineUser> OfflineUsers { get; set; }

    public ObservableCollection<GroupUser> Groups { get; set; }

    #endregion

    #region Constructors

    public UsersListModel(ObservableCollection<OnlineUser> onlineUsers, 
                          ObservableCollection<OfflineUser> offlineUsers, 
                          ObservableCollection<GroupUser> groups)
    {
      General = new GeneralUser();
      OnlineUsers = onlineUsers;
      OfflineUsers = offlineUsers;
      Groups = groups;
    }

    #endregion
  }
}
