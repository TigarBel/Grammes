namespace Common.DataBase.DataBase
{
  using System;
  using System.Collections.Generic;

  internal interface IRepository<TClass> : IDisposable
    where TClass : class 
  {
    #region Methods

    IEnumerable<TClass> GetItemList(); // получение всех объектов
    TClass GetItem(int id); // получение одного объекта по id
    void Create(TClass item); // создание объекта
    void Update(TClass item); // обновление объекта
    void Delete(int id); // удаление объекта по id
    void Save(); // сохранение изменений

    #endregion
  }
}
