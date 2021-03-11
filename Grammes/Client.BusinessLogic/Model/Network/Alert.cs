namespace Client.BusinessLogic.Model.Network
{
  using System;

  using Common.DataBaseAndNetwork.EventLog;
  using Common.Network.Messages;

  public static class Alert
  {
    #region Methods

    public static void AutomaticAlert(string login, ICurrentConnection connection)
    {
      const string ALERT = "Alert";
      var eventLogMessage = new EventLogMessage
      {
        IsSuccessfully = true,
        SenderName = login,
        Text = ALERT,
        Time = DateTime.Now,
        Type = DispatchType.EventLog
      };
      connection.Send(new MessageEventLogContainer(eventLogMessage));
    }

    #endregion
  }
}
