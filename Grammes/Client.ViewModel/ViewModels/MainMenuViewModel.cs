namespace Client.ViewModel.ViewModels
{
  using Prism.Commands;
  using Prism.Mvvm;

  public class MainMenuViewModel : BindableBase
  {
    #region Fields

    private DelegateCommand _command;

    #endregion

    #region Properties

    public DelegateCommand Command
    {
      get => _command;
      set => SetProperty(ref _command, value);
    }

    #endregion
  }
}
