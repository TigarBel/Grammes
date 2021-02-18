namespace Common.DataBase.DataBase
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;

  public class SqlUserRepository : IRepository<User>
  {
    #region Fields

    private readonly UserContext _db;

    private bool _disposed;

    #endregion

    #region Constructors

    public SqlUserRepository()
    {
      _db = new UserContext();
    }

    #endregion

    #region Methods

    public IEnumerable<User> GetItemList()
    {
      return _db.Users;
    }

    public User GetItem(int id)
    {
      return _db.Users.Find(id);
    }

    public void Create(User user)
    {
      _db.Users.Add(user);
    }

    public void Update(User user)
    {
      _db.Entry(user).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      User user = _db.Users.Find(id);
      if (user != null)
      {
        _db.Users.Remove(user);
      }
    }

    public void Save()
    {
      _db.SaveChanges();
    }

    public virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          _db.Dispose();
        }
      }

      _disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
