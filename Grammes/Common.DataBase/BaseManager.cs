namespace Common.DataBase
{
  using System.Collections.Generic;

  using DataBase;

  public class DataBaseManager
  {
    #region Properties

    public List<string> UserOnlineList { get; }

    public List<string> UserOfflineList { get; }

    #endregion

    #region Constructors

    public DataBaseManager()
    {
      UserOnlineList = new List<string>();
      var list = new UsersList();
      UserOfflineList = list.GetUsersList();
    }

    #endregion
  }
}
