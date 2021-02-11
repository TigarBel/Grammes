namespace Common.Network.Messages
{
  public class ConnectionResponseContainer : BaseContainer<Response>
  {
    #region Constructors

    public ConnectionResponseContainer()
      : base(new Response(ResponseStatus.Failure, ""), DispatchType.ConnectionResponse)
    {
    }

    #endregion
  }
}
