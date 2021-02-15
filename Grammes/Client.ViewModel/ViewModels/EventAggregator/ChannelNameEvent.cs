namespace Client.ViewModel.ViewModels.EventAggregator
{
  using Prism.Events;

  public class ChannelNameEvent : PubSubEvent<string>
  {
  }
}
