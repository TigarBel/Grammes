namespace Common.Network.Messages
{
  using _Enum_;

  public class Response
  {
    #region Properties

    public EnumResponse Result { get; }

    public string Reason { get; }

    #endregion

    #region Constructors

    public Response(EnumResponse result, string reason)
    {
      Result = result;
      Reason = reason;
    }

    #endregion
  }
}
