namespace Client.ViewModel.ViewModels.EventAggregator
{
  using Prism.Events;

  public class UserNameEvent : PubSubEvent<string>
  {
  }
}
