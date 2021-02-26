namespace Common.Network.Messages
{
  using DataBaseAndNetwork.EventLog;

  public class Container
  {
    #region Properties

    public DispatchType Identifier { get; set; }

    public object Payload { get; set; }

    #endregion
  }
}
