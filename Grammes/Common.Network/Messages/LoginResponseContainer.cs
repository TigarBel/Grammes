namespace Common.Network.Messages
{
  using System;

  public class LoginResponseContainer : BaseContainer<Response>
  {
    #region Constructors

    public LoginResponseContainer(Response response)
      : base(DispatchType.Login, response)
    {

    }

    #endregion
  }
}
