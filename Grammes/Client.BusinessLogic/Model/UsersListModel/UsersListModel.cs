namespace Client.BusinessLogic.Model.UsersListModel
{
  using System.Collections.Generic;

  public class UsersListModel
  {
    #region Properties

    public GeneralUser General { get; }

    public List<OnlineUser> OnlineList { get; set; }

    public List<OfflineUser> OfflineList { get; set; }

    public List<GroupUser> GroupList { get; set; }

    #endregion

    #region Constructors

    public UsersListModel(List<OnlineUser> onlineList, List<OfflineUser> offlineList, List<GroupUser> groupList)
    {
      General = new GeneralUser();
      OnlineList = onlineList;
      OfflineList = offlineList;
      GroupList = groupList;
    }

    #endregion
  }
}
