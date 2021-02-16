namespace Common.Network.Messages
{
  using System;

  public class LoginResponseContainer : BaseContainer<Response>
  {
    #region Constructors

    public LoginResponseContainer(DateTime timeNow, Response response)
      : base(DispatchType.Login, timeNow, response)
    {

    }

    #endregion
  }
}
