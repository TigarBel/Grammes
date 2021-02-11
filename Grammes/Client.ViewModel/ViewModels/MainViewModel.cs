namespace Client.ViewModel.ViewModels
{
  using Prism.Mvvm;

  public class MainViewModel : BindableBase
  {
    public MainMenuViewModel MainMenuViewModel { get; private set; }

    public UsersListViewModel UsersListViewModel { get; private set; }

    public MessagesViewModel.MessagesViewModel MessagesViewModel { get; private set; }

    public MainViewModel(MainMenuViewModel mainMenuViewModel, UsersListViewModel usersListViewModel, 
                         MessagesViewModel.MessagesViewModel messagesViewModel)
    {
      MainMenuViewModel = mainMenuViewModel;
      UsersListViewModel = usersListViewModel;
      MessagesViewModel = messagesViewModel;
    }
  }
}
