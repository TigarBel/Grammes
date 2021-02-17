namespace Client.BusinessLogic.Model.ChannelsListModel
{
  using System;
  using System.Collections.Generic;

  using global::Common.Network.Messages.MessageReceived;

  public class GroupChannel : BaseChannel
  {
    #region Fields

    public List<PrivateChannel> ListUsers;

    #endregion

    #region Constructors

    public GroupChannel(string name, List<PrivateChannel> listUsers)
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
