namespace Common.DataBaseAndNetwork.EventLog
{
  using System;

  public class EventLogMessage
  {
    #region Fields

    public string SenderName;

    public bool IsSuccessfully;

    public DispatchType Type;

    public string Text;

    public DateTime Time;

    #endregion
  }
}
