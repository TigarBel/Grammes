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

    public List<string> List;

    #endregion

    #region Constructors

    public UsersList()
    {
      List = new List<string>
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
  }
}
