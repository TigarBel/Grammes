namespace Common.Network.Messages.MessageReceived
{
  using System;
  using System.Collections.Generic;

  public class GroupAgenda : BaseAgenda
  {
    #region Properties

    public List<string> TargetList { get; }

    #endregion

    #region Constructors

    public GroupAgenda(List<string> targetList)
      : base(ChannelType.Group)
    {
      TargetList = targetList ?? throw new NullReferenceException("Group message received target list is null!");
    }

    #endregion
  }
}
