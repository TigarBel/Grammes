namespace Common.Network.Messages
{
  public class ConnectionRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public ConnectionRequestContainer(string login)
      : base(login, DispatchType.ConnectionRequest)
    {
    }

    #endregion
  }
}
