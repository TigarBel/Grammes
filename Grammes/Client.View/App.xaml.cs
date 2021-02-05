namespace Client.View
{
  using System.Windows;

  using Prism.Ioc;
  using Prism.Mvvm;
  using Prism.Unity;

  using UserControls;
  using UserControls.MessagesViews;

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
      containerRegistry.Register<UsersListViewModel>();
      containerRegistry.RegisterSingleton<ConnectViewModel>();
      containerRegistry.Register<MessageUserViewModel>();
    }

    protected override void ConfigureViewModelLocator()
    {
      base.ConfigureViewModelLocator();

      BindViewModelToView<ConnectViewModel, ConnectView>();
      BindViewModelToView<UsersListViewModel, UsersListView>();

      BindViewModelToView<TotalViewModel, TotalView>();

      BindViewModelToView<MessageUserViewModel, MessageUserView>();
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
