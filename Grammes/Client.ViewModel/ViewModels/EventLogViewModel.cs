namespace Client.ViewModel.ViewModels
{
  using global::Common.Network.Messages;
  using global::Common.Network.Messages.EventLog;

  using Prism.Mvvm;

  using ViewModel.Common;

  public class EventLogViewModel : BindableBase
  {
    #region Fields

    private AsyncObservableCollection<EventLogMessage> _events;

    #endregion

    #region Properties

    public AsyncObservableCollection<EventLogMessage> Events
    {
      get => _events;
      set => SetProperty(ref _events, value);
    }

    #endregion

    #region Constructors

    public EventLogViewModel()
    {
      Events = new AsyncObservableCollection<EventLogMessage>();
    }
    #endregion
  }
}
