namespace Common.Network.Messages
{
  using System;

  public class LoginRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public LoginRequestContainer(string author)
      : base(DispatchType.Login, author)
    {
    }

    #endregion
  }
}
