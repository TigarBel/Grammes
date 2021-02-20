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

    public List<User> UserList { get; private set; }

    public List<GeneralMessage> GeneralMessages { get; private set; }

    public List<PrivateMessage> PrivateMessages { get; private set; }

    #endregion

    #region Constructors

    public DataBaseManager()
    {
      UserList = new List<User>();
      GeneralMessages = new List<GeneralMessage>();
      PrivateMessages = new List<PrivateMessage>();
      GetUserList();
      GetGeneralMessages();
      GetPrivateMessages();
    }

    #endregion

    #region Methods

    private void GetUserList()
    {
      using (var db = new Unit().User) {
        UserList = db.GetAll().ToList();
      }
    }

    private void GetGeneralMessages()
    {
      using (var db = new Unit().GeneralMessage) {
        GeneralMessages = db.GetAll().ToList();
      }
    }

    private void GetPrivateMessages()
    {
      using (var db = new Unit().PrivateMessage) {
        PrivateMessages = db.GetAll().ToList();
      }
    }

    public async void CreateUserAsync(User user)
    {
      using (var db = new Unit().User) {
        await Task.Run(
          () =>
          {
            db.Add(user);
            db.Save();
            GetUserList();
          });
      }
    }

    public async void CreateGeneralMessageAsync(GeneralMessage generalMessage)
    {
      using (var db = new Unit().GeneralMessage) {
        await Task.Run(
          () =>
          {
            db.Add(generalMessage);
            db.Save();
            GetGeneralMessages();
          });
      }
    }

    public async void CreatePrivateMessageAsync(PrivateMessage privateMessage)
    {
      using (var db = new Unit().PrivateMessage) {
        await Task.Run(
          () =>
          {
            db.Add(privateMessage);
            db.Save();
            GetPrivateMessages();
          });
      }
    }

    //public async void UpdateUserAsync(User user)
    //{
    //  using (var db = new Unit().User) {
    //    await Task.Run(
    //      () =>
    //      {
    //        db.Update(user);
    //        db.Save();
    //      });
    //  }
    //}

    //public async void DeleteAsync(int id)
    //{
    //  using (var db = new SqlUserRepository()) {
    //    await Task.Run(
    //      () =>
    //      {
    //        db.Delete(id);
    //        db.Save();
    //      });
    //  }
    //}

    #endregion
  }
}
