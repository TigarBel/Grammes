namespace Client.BusinessLogic.Model.UsersListModel
{
  using System;
  using System.Collections.ObjectModel;

  public abstract class BaseUser
  {
    #region Properties

    public string Name { get; }

    public UserStatus Status { get; }


    #endregion

    #region Constructors

    protected BaseUser(string name, UserStatus status)
    {
      Name = name ?? throw new ArgumentNullException("User don't might be null!");
      Status = status;
    }

    #endregion

    public override string ToString()
    {
      return Name;
    }
  }
}
