using System.Windows;

using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;

//using Client.ViewModel.UserControls.Common;

namespace Client.View
{
  using UserControls;
  using UserControls.Common;

  using ViewModel.UserControls;

  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      //containerRegistry.RegisterSingleton<IButtonable, LeafLogic>();
      containerRegistry.Register<ConnectViewModel>();

      containerRegistry.Register<TotalViewModel>();
    }

    protected override void ConfigureViewModelLocator()
    {
      base.ConfigureViewModelLocator();

      BindViewModelToView<ConnectViewModel, ConnectView>();

      BindViewModelToView<TotalViewModel, TotalView>();
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
  }
}
