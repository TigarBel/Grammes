namespace Common.Network.Messages.Channels
{
  public class UpdateChannel
  {
    #region Properties

    public bool IsConnect { get; }

    public string Login { get; }

    public bool IsRegistration { get; }

    #endregion

    #region Constructors

    public UpdateChannel(bool isConnect, string login, bool isRegistration = false)
    {
      IsConnect = isConnect;
      Login = login;
      IsRegistration = isRegistration;
    }

    #endregion
  }
}
