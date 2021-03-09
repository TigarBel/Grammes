namespace Common.DataBase
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using DataBase;
  using DataBase.Repository;
  using DataBase.Table;

  public class DataBaseManager
  {
    #region Fields

    private readonly string _dataSource;

    private readonly string _catalog;

    #endregion

    #region Properties

    public List<User> UserList { get; private set; }

    #endregion

    #region Constructors

    public DataBaseManager(string dataSource, string catalog)
    {
      _dataSource = dataSource;
      _catalog = catalog;
      UserList = new List<User>();
      GetUserList();
    }

    #endregion

    #region Methods

    public async void CreateUserAsync(User user)
    {
      using (UserRepository db = GetUnit().User)
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
      using (GeneralMessageRepository db = GetUnit().GeneralMessage)
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
      using (PrivateMessageRepository db = GetUnit().PrivateMessage)
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
      using (EventRepository db = GetUnit().Event)
      {
        await Task.Run(
          () =>
          {
            db.Add(localEvent);
            db.Save();
          });
      }
    }

    public async Task<List<GeneralMessage>> GetGeneralMessageAsync()
    {
      var returnGeneralMessage = new List<GeneralMessage>();
      using (GeneralMessageRepository db = GetUnit().GeneralMessage)
      {
        await Task.Run(
          () =>
          {
            foreach (GeneralMessage message in db.GetAll())
            {
              db.GrammesDbContext.Entry(message).Reference(u => u.User).Load();
              returnGeneralMessage.Add(message);
            }
          });
      }

      return returnGeneralMessage;
    }

    public async Task<List<PrivateMessage>> GetPrivateMessageAsync()
    {
      var returnPrivateMessage = new List<PrivateMessage>();
      using (PrivateMessageRepository db = GetUnit().PrivateMessage)
      {
        await Task.Run(
          () =>
          {
            foreach (PrivateMessage message in db.GetAll())
            {
              db.GrammesDbContext.Entry(message).Reference(u => u.Sender).Load();
              db.GrammesDbContext.Entry(message).Reference(u => u.Target).Load();
              returnPrivateMessage.Add(message);
            }
          });
      }

      return returnPrivateMessage;
    }

    public async Task<List<Event>> GetEventAsync()
    {
      var events = new List<Event>();
      using (EventRepository db = GetUnit().Event) {
        await Task.Run(
          () =>
          {
            foreach (Event unit in db.GetAll()) {
              events.Add(unit);
            }
          });
      }

      return events;
    }

    private Unit GetUnit()
    {
      return new Unit(_dataSource, _catalog);
    }

    private void GetUserList()
    {
      bool onRestart = true;
      Task.Run(
        async () =>
        {
          await Task.Delay(TimeSpan.FromSeconds(30));
          if (onRestart)
          {
            throw new TimeoutException("Overtime load database!");
          }
        });
      using (UserRepository db = GetUnit().User)
      {
        UserList = db.GetAll().ToList();
      }

      onRestart = false;
    }

    #endregion
  }
}
