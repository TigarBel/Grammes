namespace Common.Network.Messages
{
  using System;

  public class ConnectionRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public ConnectionRequestContainer(DateTime timeNow, string author)
      : base(DispatchType.ConnectionRequest, timeNow, author)
    {
    }

    #endregion
  }
}
