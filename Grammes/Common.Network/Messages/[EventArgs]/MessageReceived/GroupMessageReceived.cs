namespace Common.Network.Messages.MessageReceived
{
  using System;
  using System.Collections.Generic;

  public class GroupMessageReceived : BaseMessageReceived
  {
    #region Properties

    public List<string> TargetList { get; }

    #endregion

    #region Constructors

    public GroupMessageReceived(List<string> targetList)
      : base(MessageReceivedType.Group)
    {
      TargetList = targetList ?? throw new NullReferenceException("Group message received target list is null!");
    }

    #endregion
  }
}
