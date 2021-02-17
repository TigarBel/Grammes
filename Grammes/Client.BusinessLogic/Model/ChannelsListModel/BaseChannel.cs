namespace Client.BusinessLogic.Model.ChannelsListModel
{
  using System;

  public abstract class BaseChannel
  {
    #region Properties

    public string Name { get; private set; }

    public ChannelType Type { get; private set; }

    #endregion

    #region Constructors

    protected BaseChannel(string name, ChannelType type)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException("User name don't might be null or empty!");
      }
      Name = name;
      Type = type;
    }

    #endregion

    #region Methods

    public override string ToString()
    {
      return Name;
    }

    #endregion
  }
}
