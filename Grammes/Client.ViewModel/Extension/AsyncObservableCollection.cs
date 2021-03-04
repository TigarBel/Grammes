namespace Client.ViewModel.Extension
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Threading;

  public class AsyncObservableCollection<T> : ObservableCollection<T>
  {
    #region Fields

    private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

    #endregion

    #region Constructors

    public AsyncObservableCollection()
    {
    }

    public AsyncObservableCollection(IEnumerable<T> list)
      : base(list)
    {
    }

    #endregion

    #region Methods

    protected override void InsertItem(int index, T item)
    {
      ExecuteOnSyncContext(() => base.InsertItem(index, item));
    }

    protected override void RemoveItem(int index)
    {
      ExecuteOnSyncContext(() => base.RemoveItem(index));
    }

    protected override void SetItem(int index, T item)
    {
      ExecuteOnSyncContext(() => base.SetItem(index, item));
    }

    protected override void MoveItem(int oldIndex, int newIndex)
    {
      ExecuteOnSyncContext(() => base.MoveItem(oldIndex, newIndex));
    }

    protected override void ClearItems()
    {
      ExecuteOnSyncContext(() => base.ClearItems());
    }

    private void ExecuteOnSyncContext(Action action)
    {
      if (SynchronizationContext.Current == _synchronizationContext)
      {
        action();
      }
      else
      {
        _synchronizationContext.Send(_ => action(), null);
      }
    }

    #endregion
  }
}
