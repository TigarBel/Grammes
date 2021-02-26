namespace Server.BusinessLogic.Config
{
  using System.Net;

  public class Config
  {
    #region Properties

    public IPEndPoint Address { get; }

    public uint Timeout { get; }

    #endregion

    #region Constructors

    public Config(IPEndPoint address, uint timeOut)
    {
      Address = address;
      Timeout = timeOut;
    }

    #endregion
  }
}
