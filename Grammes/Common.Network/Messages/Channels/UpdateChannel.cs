namespace Common.Network.Messages.Channels
{
  public class UpdateChannel
  {
    #region Properties

    public bool IsConnect { get; }

    public string Login { get; }

    #endregion

    #region Constructors

    public UpdateChannel(bool isConnect, string login)
    {
      IsConnect = isConnect;
      Login = login;
    }

    #endregion
  }
}
