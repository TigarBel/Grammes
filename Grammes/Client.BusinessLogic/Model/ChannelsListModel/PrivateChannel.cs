﻿namespace Client.BusinessLogic.Model.ChannelsListModel
{
  using global::Common.Network.Messages.MessageReceived;

  public abstract class PrivateChannel : BaseChannel
  {
    #region Fields

    public bool IsOnline { get; private set; }

    #endregion

    #region Constructors

    protected PrivateChannel(string name, bool isOnline)
      : base(name, ChannelType.Private)
    {
      IsOnline = isOnline;
    }

    #endregion
  }
}