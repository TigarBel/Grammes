namespace Common.Network.Messages
{
  using _Enum_;

  public class MessageContainer
  {
    #region Properties

    public EnumRequest Identifier { get; set; }

    public object Payload { get; set; }

    #endregion
  }
}
