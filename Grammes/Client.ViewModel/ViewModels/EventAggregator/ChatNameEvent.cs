﻿namespace Client.ViewModel.ViewModels.EventAggregator
{
  using Prism.Events;

  public class ChatNameEvent : PubSubEvent<string>
  {
  }
}