namespace Common.DataBase.DataBase
{
  using System.Collections.Generic;

  public class UsersList
  {
    #region Fields

    private readonly List<string> _list;

    #endregion

    #region Constructors

    public UsersList()
    {
      _list = new List<string>
      {
        "123",
        "321",
        "User1",
        "User2",
        "User3",
        "User4",
        "User5"
      };
    }

    #endregion

    #region Methods

    public List<string> GetUsersList()
    {
      var usersList = new List<string>();
      foreach (string user in _list)
      {
        usersList.Add(user);
      }

      return usersList;
    }

    #endregion
  }
}
