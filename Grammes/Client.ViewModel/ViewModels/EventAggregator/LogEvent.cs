namespace Client.ViewModel.ViewModels.EventAggregator
{
  using Common.DataBaseAndNetwork.EventLog;

  using Prism.Events;

  public class LogEvent : PubSubEvent<EventLogMessage>
  {
  }
}
