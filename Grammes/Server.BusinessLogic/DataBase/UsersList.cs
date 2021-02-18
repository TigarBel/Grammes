using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLogic.DataBase
{
  public class UsersList
  {
    #region Fields

    private List<string> _list;

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

    public List<string> GetUsersList()
    {
      List<string> usersList = new List<string>();
      foreach (var user in _list)
      {
        usersList.Add(user);
      }

      return usersList;
    }

    #endregion
  }
}
