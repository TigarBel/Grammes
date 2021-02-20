namespace Common.DataBase.DataBase
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  internal interface IRepository<TClass> : IDisposable
    where TClass : class 
  {
    #region Methods

    IEnumerable<TClass> GetAll(); // получение всех объектов
    TClass Get(int id); // получение одного объекта по id
    IEnumerable<TClass> Find(Expression<Func<TClass, bool>> predicate);
    void Add(TClass item); // создание объекта
    void AddRange(IEnumerable<TClass> items);
    void Update(TClass item); // обновление объекта
    void Remove(TClass item); // удаление объекта
    void RemoveRange(IEnumerable<TClass> items);
    void Save(); // сохранение изменений

    #endregion
  }
}
