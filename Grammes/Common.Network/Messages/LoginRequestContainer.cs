namespace Common.Network.Messages
{
  using System;

  public class LoginRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public LoginRequestContainer(DateTime timeNow, string author)
      : base(DispatchType.Login, timeNow, author)
    {
    }

    #endregion
  }
}
