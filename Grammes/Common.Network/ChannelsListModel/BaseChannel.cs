namespace Common.Network.ChannelsListModel
{
  using System;
  using System.Collections.Generic;

  using Messages.MessageReceived;

  public abstract class BaseChannel
  {
    #region Fields

    public List<MessageModel> MessageList = new List<MessageModel>();

    #endregion

    #region Properties

    public string Name { get; }

    public ChannelType Type { get; }

    #endregion

    #region Constructors

    protected BaseChannel(string name, ChannelType type)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException("User name don't might be null or empty!");
      }

      Name = name;
      Type = type;
    }

    #endregion

    #region Methods

    public override string ToString()
    {
      return Name;
    }

    #endregion
  }
}
