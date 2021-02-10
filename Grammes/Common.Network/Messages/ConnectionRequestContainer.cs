namespace Common.Network.Messages
{
  using _Enum_;

  public class ConnectionRequest : BaseContainer<string>
  {
    #region Constructors

    public ConnectionRequest(string login)
      : base(login, EnumRequest.ConnectionRequest)
    {
    }

    #endregion
  }
}
