namespace Client.ViewModel.ViewModels.EventAggregator
{
  using global::Common.Network.ChannelsListModel;

  using Prism.Events;

  public class ChannelNameEvent : PubSubEvent<BaseChannel>
  {
  }
}
