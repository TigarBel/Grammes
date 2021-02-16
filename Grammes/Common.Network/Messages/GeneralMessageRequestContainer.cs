namespace Common.Network.Messages
{
  using System;

  public class GeneralMessageRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public GeneralMessageRequestContainer(string author, DateTime timeNow, string message)
      : base(DispatchType.Message, timeNow, message)
    {
      _author = author;
    }

    #endregion
  }
}
