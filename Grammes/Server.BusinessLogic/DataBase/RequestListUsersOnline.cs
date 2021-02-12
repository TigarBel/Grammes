namespace Server.BusinessLogic.DataBase
{
  using System.Collections.Generic;

  public class RequestListUsersOnline
  {
    #region Fields

    private readonly List<string> _usersList = new List<string>();

    #endregion

    #region Constructors

    public RequestListUsersOnline()
    {
      _usersList.Add("User1");
      _usersList.Add("User2");
      _usersList.Add("User3");
      _usersList.Add("User4");
      _usersList.Add("User5");
    }

    #endregion

    #region Methods

    public List<string> GetUsersList()
    {
      return _usersList;
    }

    #endregion
  }
}
