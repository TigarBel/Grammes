namespace Common.Network.Messages
{
  using System;

  public class MessageRequestContainer : BaseContainer<string>
  {
    #region Constructors

    public MessageRequestContainer(string author, string target, DateTime timeNow, string message)
      : base(DispatchType.Message, timeNow, message)
    {
      _author = author;
      _target = target;
    }

    #endregion
  }
}
