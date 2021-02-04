namespace Client.BusinessLogic.Model.UsersListModel
{
  using System;

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

    #region Methods

    public override string ToString()
    {
      return Name;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as BaseUser);
    }

    public bool Equals(BaseUser user)
    {
      return user != null && Name == user.Name && Status == user.Status;
    }

    #endregion
  }
}
