namespace Common.DataBaseAndNetwork.EventLog
{
  using System;

  public class EventLogMessage
  {
    #region Properties

    public string SenderName { get; }

    public bool IsSuccessfully { get; set; }

    public DispatchType Type { get; }

    public string Text { get; set; }

    public DateTime Time { get; }

    #endregion

    #region Constructors

    public EventLogMessage(string sender, bool isSuccessfully, DispatchType type, string text, DateTime time)
    {
      SenderName = sender;
      IsSuccessfully = isSuccessfully;
      Type = type;
      Text = text;
      Time = time;
    }

    #endregion
  }
}
