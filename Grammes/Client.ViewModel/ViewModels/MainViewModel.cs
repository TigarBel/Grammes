namespace Client.ViewModel.ViewModels
{
  using global::Common.Network.Messages.EventLog;

  using Prism.Mvvm;

  using ViewModel.Common;

  public class MainViewModel : BindableBase
  {
    #region Fields

    private AsyncObservableCollection<EventLogMessage> _events;

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

    #endregion

    #region Constructors

    public MainViewModel(
      MainMenuViewModel mainMenuViewModel,
      MessagesViewModel.MessagesViewModel messagesViewModel,
      UsersListViewModel usersListViewModel)
    {
      MainMenuViewModel = mainMenuViewModel;
      MessagesViewModel = messagesViewModel;
      UsersListViewModel = usersListViewModel;
      Events = new AsyncObservableCollection<EventLogMessage>();
    }

    #endregion

    #region Methods

    public void Clear()
    {
      MessagesViewModel.MessagesUserList.Clear();
    }

    #endregion
  }
}
