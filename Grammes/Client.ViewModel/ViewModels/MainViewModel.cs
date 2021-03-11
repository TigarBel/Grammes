namespace Client.ViewModel.ViewModels
{
  using System.Collections.ObjectModel;
  using System.Linq;

  using BusinessLogic.Model.Network;

  using Common.DataBaseAndNetwork.EventLog;

  using EventAggregator;

  using Extension;

  using Prism.Commands;
  using Prism.Events;
  using Prism.Mvvm;

  public class MainViewModel : BindableBase
  {
    #region Constants

    private const string ALL = "A||";

    #endregion

    #region Fields

    private ObservableCollection<EventLogMessage> _events;

    private readonly AsyncObservableCollection<EventLogMessage> _allEvents;

    private AsyncObservableCollection<string> _nameFilter;

    private string _selectName;

    private DelegateCommand _mouseClickCommand;

    private string _loginName;

    #endregion

    #region Properties

    public MainMenuViewModel MainMenuViewModel { get; }

    public UsersListViewModel UsersListViewModel { get; }

    public MessagesViewModel.MessagesViewModel MessagesViewModel { get; }

    public ObservableCollection<EventLogMessage> Events
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
        Events = value == ALL ? _allEvents : new ObservableCollection<EventLogMessage>(_allEvents.Where(log => log.SenderName == value).ToList());
      }
    }

    public DelegateCommand MouseClickCommand
    {
      get => _mouseClickCommand;
      set => SetProperty(ref _mouseClickCommand, value);
    }

    #endregion

    #region Constructors

    public MainViewModel(
      MainMenuViewModel mainMenuViewModel,
      MessagesViewModel.MessagesViewModel messagesViewModel,
      UsersListViewModel usersListViewModel,
      IEventAggregator eventAggregator,
      ICurrentConnection currentConnection)
    {
      MainMenuViewModel = mainMenuViewModel;
      MessagesViewModel = messagesViewModel;
      UsersListViewModel = usersListViewModel;
      _allEvents = new AsyncObservableCollection<EventLogMessage>();
      NameFilter = new AsyncObservableCollection<string>();
      InitNameFilter();
      eventAggregator.GetEvent<LogEvent>().Subscribe(SetEventLog);
      eventAggregator.GetEvent<LoginNameEvent>().Subscribe(SetClient);
      MouseClickCommand = new DelegateCommand(
        () =>
        {
          Alert.AutomaticAlert(_loginName, currentConnection);
        });
    }

    #endregion

    #region Methods

    public void Clear()
    {
      Events.Clear();
      _allEvents.Clear();
      InitNameFilter();
      MessagesViewModel.MessagesUserList.Clear();
      MessagesViewModel.TextMessage = string.Empty;
    }

    private void SetClient(string client)
    {
      _loginName = client;
    }

    private void InitNameFilter()
    {
      NameFilter.Clear();
      NameFilter.Add(ALL);
      SelectName = ALL;
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
