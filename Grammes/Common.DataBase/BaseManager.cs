namespace Common.DataBase
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using DataBase;
  using DataBase.Repository;
  using DataBase.Table;

  using DataBaseAndNetwork.EventLog;

  public class DataBaseManager
  {
    #region Properties

    public List<User> UserList { get; private set; }

    #endregion

    #region Constructors

    public DataBaseManager()
    {
      UserList = new List<User>();
      GetUserList();
    }

    #endregion

    #region Methods

    public async void CreateUserAsync(User user)
    {
      using (UserRepository db = new Unit().User)
      {
        await Task.Run(
          () =>
          {
            db.Add(user);
            db.Save();
            UserList.Add(user);
          });
      }
    }

    public async void CreateGeneralMessageAsync(GeneralMessage generalMessage)
    {
      using (GeneralMessageRepository db = new Unit().GeneralMessage)
      {
        await Task.Run(
          () =>
          {
            db.Add(generalMessage);
            db.Save();
          });
      }
    }

    public async void CreatePrivateMessageAsync(PrivateMessage privateMessage)
    {
      using (PrivateMessageRepository db = new Unit().PrivateMessage)
      {
        await Task.Run(
          () =>
          {
            db.Add(privateMessage);
            db.Save();
          });
      }
    }


    public async void CreateEventAsync(Event localEvent)
    {
      using (EventRepository db = new Unit().Event) {
        await Task.Run(
          () =>
          {
            db.Add(localEvent);
            db.Save();
          });
      }
    }

    public async Task<User> GetUserAsync(int id)
    {
      User returnUser = null;
      using (UserRepository db = new Unit().User)
      {
        await Task.Run(
          () =>
          {
            returnUser = db.Find(u => u.Id == id).FirstOrDefault();
            db.GrammesDbContext.Entry(returnUser).Collection(p => p.PrivateMessages).Load();
            db.GrammesDbContext.Entry(returnUser).Collection(b => b.Bands).Load();
          });
      }

      return returnUser;
    }

    public async Task<List<GeneralMessage>> GetGeneralMessageAsync()
    {
      List<GeneralMessage> returnGeneralMessage = new List<GeneralMessage>();
      using (var db = new Unit().GeneralMessage) {
        await Task.Run(
          () =>
          {
            foreach (var message in db.GetAll())
            {
              db.GrammesDbContext.Entry(message).Reference(u=>u.User).Load();
              returnGeneralMessage.Add(message);
            }
          });
      }

      return returnGeneralMessage;
    }

    public async Task<List<PrivateMessage>> GetPrivateMessageAsync()
    {
      List<PrivateMessage> returnPrivateMessage = new List<PrivateMessage>();
      using (var db = new Unit().PrivateMessage) {
        await Task.Run(
          () =>
          {
            foreach (var message in db.GetAll()) {
              db.GrammesDbContext.Entry(message).Reference(u => u.Sender).Load();
              db.GrammesDbContext.Entry(message).Reference(u => u.Target).Load();
              returnPrivateMessage.Add(message);
            }
          });
      }

      return returnPrivateMessage;
    }

    private void GetUserList()
    {
      using (UserRepository db = new Unit().User)
      {
        UserList = db.GetAll().ToList();
      }
    }

    #endregion
  }
}
