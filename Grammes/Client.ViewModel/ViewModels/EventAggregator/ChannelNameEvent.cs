namespace Client.ViewModel.ViewModels.EventAggregator
{
  using BusinessLogic.Model.ChannelsListModel;

  using Prism.Events;

  public class ChannelNameEvent : PubSubEvent<BaseChannel>
  {
  }
}
