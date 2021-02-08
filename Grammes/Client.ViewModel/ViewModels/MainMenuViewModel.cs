namespace Client.ViewModel.ViewModels
{
  using Prism.Commands;
  using Prism.Mvvm;

  public class MainMenuViewModel : BindableBase
  {
    private DelegateCommand _command;

    public DelegateCommand Command
    {
      get => _command;
      set => SetProperty(ref _command, value);
    }
  }
}
