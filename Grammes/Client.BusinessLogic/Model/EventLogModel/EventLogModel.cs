namespace Client.BusinessLogic.Model.EventLogModel
{
  using System;

  public class EventLogModel
  {
    #region Properties

    public string Event { get; set; }

    public bool IsSuccessfully { get; set; }

    public DateTime Time { get; set; }

    #endregion
  }
}
