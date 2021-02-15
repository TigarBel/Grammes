namespace Common.Network.Messages
{
  using System;

  public class ConnectionResponseContainer : BaseContainer<Response>
  {
    #region Constructors

    public ConnectionResponseContainer(DateTime timeNow, Response response)
      : base(DispatchType.ConnectionResponse, timeNow, response)
    {

    }

    #endregion
  }
}
