namespace Client.ViewModel.ViewModels.EventAggregator
{
  using global::Common.Network.Messages;

  using Prism.Events;

  public class MessageReceivedEvent : PubSubEvent<MessageReceivedEventArgs>
  {
  }
}
