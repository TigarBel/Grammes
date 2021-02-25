namespace Common.DataBase.DataBase
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using System.Linq.Expressions;

  public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
  {
    #region Fields

    protected readonly DbContext _context;

    private bool _disposed;

    #endregion

    #region Constructors

    public Repository(DbContext context)
    {
      _context = context;
    }

    #endregion

    #region Methods

    public IEnumerable<TEntity> GetAll()
    {
      return _context.Set<TEntity>().ToList();
    }

    public TEntity Get(int id)
    {
      return _context.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return _context.Set<TEntity>().Where(predicate);
    }

    public void Add(TEntity entity)
    {
      _context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
      _context.Set<TEntity>().AddRange(entities);
    }

    public void Update(TEntity entity)
    {
      _context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(TEntity entity)
    {
      _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
      _context.Set<TEntity>().RemoveRange(entities);
    }

    public void Save()
    {
      _context.SaveChanges();
    }

    public void Dispose()
    {
      if (!_disposed) {
        _context.Dispose();
      }

      _disposed = true;
    }

    #endregion
  }
}
