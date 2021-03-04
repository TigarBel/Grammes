namespace Common.DataBaseAndNetwork.EventLog
{
  using System;

  public class EventLogMessage
  {
    #region Fields

    public string SenderName { get; set; }

    public bool IsSuccessfully { get; set; }

    public DispatchType Type { get; set; }

    public string Text { get; set; }

    public DateTime Time { get; set; }

    #endregion
  }
}
