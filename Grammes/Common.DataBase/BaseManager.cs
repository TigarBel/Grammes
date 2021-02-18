namespace Common.DataBase
{
  using System.Collections.Generic;

  using DataBase;

  public class BaseManager
  {
    #region Properties

    public List<string> UserOnlineList { get; }

    public List<string> UserOfflineList { get; }

    #endregion

    #region Constructors

    public BaseManager()
    {
      UserOnlineList = new List<string>();
      var list = new UsersList();
      UserOfflineList = list.GetUsersList();
    }

    #endregion
  }
}
