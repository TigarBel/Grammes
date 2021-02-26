namespace Client.ViewModel.ViewModels
{
  using System.Linq;

  using Common.DataBaseAndNetwork.EventLog;

  using EventAggregator;

  using Extension;

  using Prism.Events;
  using Prism.Mvvm;

  public class MainViewModel : BindableBase
  {
    #region Constants

    private const string ALL = "A||";

    #endregion

    #region Fields

    private AsyncObservableCollection<EventLogMessage> _events;

    private readonly AsyncObservableCollection<EventLogMessage> _allEvents;

    private AsyncObservableCollection<string> _nameFilter;

    private string _selectName;

    private readonly IEventAggregator _logEa;

    #endregion

    #region Properties

    public MainMenuViewModel MainMenuViewModel { get; }

    public UsersListViewModel UsersListViewModel { get; }

    public MessagesViewModel.MessagesViewModel MessagesViewModel { get; }

    public AsyncObservableCollection<EventLogMessage> Events
    {
      get => _events;
      set => SetProperty(ref _events, value);
    }

    public AsyncObservableCollection<string> NameFilter
    {
      get => _nameFilter;
      set => SetProperty(ref _nameFilter, value);
    }

    public string SelectName
    {
      get => _selectName;
      set
      {
        SetProperty(ref _selectName, value);
        Events = value == ALL
                   ? _allEvents
                   : new AsyncObservableCollection<EventLogMessage>(_allEvents.Where(log => log.SenderName == value).ToList());
      }
    }

    #endregion

    #region Constructors

    public MainViewModel(
      MainMenuViewModel mainMenuViewModel,
      MessagesViewModel.MessagesViewModel messagesViewModel,
      UsersListViewModel usersListViewModel,
      IEventAggregator eventAggregator)
    {
      MainMenuViewModel = mainMenuViewModel;
      MessagesViewModel = messagesViewModel;
      UsersListViewModel = usersListViewModel;
      _allEvents = new AsyncObservableCollection<EventLogMessage>();
      NameFilter = new AsyncObservableCollection<string>
      {
        ALL
      };
      SelectName = ALL;
      _logEa = eventAggregator;
      eventAggregator.GetEvent<LogEvent>().Subscribe(SetEventLog);
    }

    #endregion

    #region Methods

    public void Clear()
    {
      Events.Clear();
      MessagesViewModel.MessagesUserList.Clear();
    }

    private void SetEventLog(EventLogMessage eventLog)
    {
      _allEvents.Add(eventLog);
      if (NameFilter.All(name => name != eventLog.SenderName))
      {
        NameFilter.Add(eventLog.SenderName);
      }
    }

    #endregion
  }
}
