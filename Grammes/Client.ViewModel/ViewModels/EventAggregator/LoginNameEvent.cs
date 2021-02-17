namespace Client.ViewModel.ViewModels.EventAggregator
{
  using Prism.Events;

  public class LoginNameEvent : PubSubEvent<string>
  {
  }
}
