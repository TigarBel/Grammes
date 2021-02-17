namespace Common.Network.Messages.MessageReceived
{
  using System;
  using System.Collections.Generic;

  public class GroupAgenda : BaseAgenda
  {
    #region Properties

    public string GroupName { get; }

    public List<string> TargetList { get; }

    #endregion

    #region Constructors

    public GroupAgenda(string groupName, List<string> targetList)
      : base(ChannelType.Group)
    {
      GroupName = groupName ?? throw new NullReferenceException("Group name null!");
      TargetList = targetList ?? throw new NullReferenceException("Group message received target list is null!");
    }

    #endregion
  }
}
