namespace Common.Network.Messages
{
  using System;

  public class GeneralMessageContainer : BaseContainer<string>
  {
    #region Constructors

    public GeneralMessageContainer(string author, string message)
      : base(DispatchType.Message, message)
    {
      Author = author;
    }

    #endregion
  }
}
