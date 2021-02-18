namespace Common.DataBase.DataBase
{
  using System.Data.Entity;

  internal class UserContext : DbContext
  {
    #region Properties

    public DbSet<User> Users { get; set; }

    #endregion

    #region Constructors

    public UserContext()
      : base("DbConnection")
    {
    }

    #endregion
  }
}
