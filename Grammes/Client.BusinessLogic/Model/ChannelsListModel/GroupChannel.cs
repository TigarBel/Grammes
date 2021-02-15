namespace Client.BusinessLogic.Model.ChannelsListModel
{
  using System;
  using System.Collections.Generic;

  public class GroupChannel : BaseChannel
  {
    #region Fields

    public List<UserChannel> ListUsers;

    #endregion

    #region Constructors

    public GroupChannel(string name, List<UserChannel> listUsers)
      : base(name, ChannelType.Group)
    {
      if (listUsers == null)
      {
        throw new NullReferenceException("List group is empty");
      }

      if (listUsers.Count < 2)
      {
        throw new ArgumentException("Users group in list must be more than 1");
      }

      ListUsers = listUsers;
    }

    #endregion
  }
}
