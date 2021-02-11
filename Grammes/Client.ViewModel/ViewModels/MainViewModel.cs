namespace Client.ViewModel.ViewModels
{
  using Prism.Mvvm;

  public class MainViewModel : BindableBase
  {
    public MainMenuViewModel MainMenuViewModel { get; private set; }

    public UsersListViewModel UsersListViewModel { get; private set; }

    public MessagesViewModel.MessagesViewModel MessagesViewModel { get; private set; }

    public MainViewModel(MainMenuViewModel mainMenuViewModel, MessagesViewModel.MessagesViewModel messagesViewModel,
                         UsersListViewModel usersListViewModel)
    {
      MainMenuViewModel = mainMenuViewModel;
      MessagesViewModel = messagesViewModel;
      UsersListViewModel = usersListViewModel;
    }
  }
}
