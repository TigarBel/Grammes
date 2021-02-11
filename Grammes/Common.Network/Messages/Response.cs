namespace Common.Network.Messages
{
  public class Response
  {
    #region Properties

    public ResponseStatus Result { get; }

    public string Reason { get; }

    #endregion

    #region Constructors

    public Response(ResponseStatus result, string reason)
    {
      Result = result;
      Reason = reason;
    }

    #endregion
  }
}
