namespace Client.ViewModel.ViewModels.EventAggregator
{
  using Common.Network.Messages.EventLog;

  using Prism.Events;

  public class LogEvent : PubSubEvent<EventLogMessage>
  {
  }
}
