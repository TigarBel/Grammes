namespace Common.Network.Messages
{

  public class Response
  {
    #region Properties

    public ResponseType Result { get; }

    public string Reason { get; }

    #endregion

    #region Constructors

    public Response(ResponseType result, string reason)
    {
      Result = result;
      Reason = reason;
    }

    #endregion
  }
}
