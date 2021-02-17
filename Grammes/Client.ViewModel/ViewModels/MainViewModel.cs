namespace Client.ViewModel.ViewModels
{
  using Prism.Mvvm;

  public class MainViewModel : BindableBase
  {
    #region Properties

    public MainMenuViewModel MainMenuViewModel { get; }

    public UsersListViewModel UsersListViewModel { get; }

    public MessagesViewModel.MessagesViewModel MessagesViewModel { get; }

    public EventLogViewModel EventLogViewModel { get; }

    #endregion

    #region Constructors

    public MainViewModel(
      MainMenuViewModel mainMenuViewModel,
      MessagesViewModel.MessagesViewModel messagesViewModel,
      UsersListViewModel usersListViewModel,
      EventLogViewModel eventLogViewModel)
    {
      MainMenuViewModel = mainMenuViewModel;
      MessagesViewModel = messagesViewModel;
      UsersListViewModel = usersListViewModel;
      EventLogViewModel = eventLogViewModel;
    }

    public void Clear()
    {
      MessagesViewModel.MessagesUserList.Clear();
      EventLogViewModel.Events.Clear();
    }

    #endregion
  }
}
