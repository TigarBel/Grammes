namespace Common.DataBase.DataBase
{
  using System;

  using Repository;

  public class Unit
  {
    #region Fields

    #endregion

    #region Properties

    public UserRepository User { get; }

    public GeneralMessageRepository GeneralMessage { get; }

    public PrivateMessageRepository PrivateMessage { get; }

    public EventRepository Event { get; }

    #endregion

    #region Constructors

    public Unit()
    {
      var context = new GrammesDbContext("DbConnection");
      User = new UserRepository(context);
      GeneralMessage = new GeneralMessageRepository(context);
      PrivateMessage = new PrivateMessageRepository(context);
      Event = new EventRepository(context);
    }

    #endregion
  }
}
