namespace Common.Network.Messages
{
  using EventLog;

  using MessageReceived;

  public class UpdateChannelEventArgs
  {
    #region Properties

    public string ChannelName { get; }

    public bool Connected { get; }

    public bool IsNewLogin { get; }

    public ChannelType Type { get; }

    public EventLogMessage EventLog { get; }

    #endregion

    #region Constructors

    public UpdateChannelEventArgs(string channelName, bool connected, EventLogMessage eventLog, bool isNewLogin = false)
    {
      ChannelName = channelName;
      Connected = connected;
      IsNewLogin = isNewLogin;
      Type = ChannelType.Private;
      EventLog = eventLog;
    }

    #endregion
  }
}
