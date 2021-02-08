namespace Client.View
{
  using System.Windows;

  using Prism.Ioc;
  using Prism.Mvvm;
  using Prism.Unity;

  using UserControls;

  using ViewModel.ViewModels;
  using ViewModel.ViewModels.MessagesViewModel;

  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    #region Methods

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.Register<TotalViewModel>();

      containerRegistry.RegisterSingleton<ConnectViewModel>();


      containerRegistry.Register<UsersListMessagesViewModel>();

      containerRegistry.RegisterSingleton<MainMenuViewModel>();
      containerRegistry.RegisterSingleton<MessagesViewModel>();
      containerRegistry.RegisterSingleton<UsersListViewModel>();
      containerRegistry.Register<EventLogViewModel>();
    }

    protected override void ConfigureViewModelLocator()
    {
      base.ConfigureViewModelLocator();
      BindViewModelToView<TotalViewModel, TotalView>();
      BindViewModelToView<ConnectViewModel, ConnectView>();

      BindViewModelToView<MainMenuViewModel, MainMenuView>();
      BindViewModelToView<UsersListMessagesViewModel, UsersListMessagesView>();
      BindViewModelToView<UsersListViewModel, UsersListView>();
      BindViewModelToView<MessagesViewModel, MessagesView>();
      BindViewModelToView<EventLogViewModel, EventLogView>();
    }

    protected override Window CreateShell()
    {
      var mainView = Container.Resolve<MainWindow>();
      return mainView;
    }

    private void BindViewModelToView<TViewModel, TView>()
    {
      ViewModelLocationProvider.Register(typeof(TView).ToString(), () => Container.Resolve<TViewModel>());
    }

    #endregion
  }
}
