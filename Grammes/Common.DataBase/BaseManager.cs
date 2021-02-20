namespace Common.DataBase
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using DataBase;
  using DataBase.Table;

  public class DataBaseManager
  {
    #region Properties

    public List<User> UserOnlineList { get; set; }

    public List<User> UserOfflineList { get; set; }

    #endregion

    #region Constructors

    public DataBaseManager()
    {
      UserOnlineList = new List<User>();
      UserOfflineList = new List<User>();
      GetUserListAsync();
    }

    #endregion

    #region Methods

    public async Task<User> GetUserAsync(int id)
    {
      using (var db = new SqlUserRepository())
      {
        return await Task.Run(() => db.GetItem(id));
      }
    }

    public async void CreateAsync(User user)
    {
      using (var db = new SqlUserRepository())
      {
        await Task.Run(
          () =>
          {
            db.Create(user);
          });
      }
    }

    public async void UpdateAsync(User user)
    {
      using (var db = new SqlUserRepository())
      {
        await Task.Run(
          () =>
          {
            db.Update(user);
          });
      }
    }

    public async void DeleteAsync(int id)
    {
      using (var db = new SqlUserRepository())
      {
        await Task.Run(
          () =>
          {
            db.Delete(id);
          });
      }
    }

    public async void SaveAsync()
    {
      using (var db = new SqlUserRepository())
      {
        await Task.Run(
          () =>
          {
            db.Save();
          });
      }
    }

    private async void GetUserListAsync()
    {
      using (var db = new SqlUserRepository())
      {
        await Task.Run(
          () =>
          {
            UserOfflineList = db.GetItemList().ToList();
          });
      }
    }

    #endregion
  }
}
