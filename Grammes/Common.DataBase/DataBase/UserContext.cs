namespace Common.DataBase.DataBase
{
  using System.Data.Entity;

  using Table;

  internal class UserContext : DbContext
  {
    #region Properties

    public DbSet<User> Users { get; set; }
    public DbSet<GeneralMessage> GeneralMessages { get; set; }
    public DbSet<PrivateMessage> PrivateMessages { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<GroupMessage> GroupMessages { get; set; }

    #endregion

    #region Constructors

    public UserContext()
      : base("DbConnection")
    {
    }

    #endregion

    #region Methods

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasIndex(i => i.Name).IsUnique();
      modelBuilder.Entity<Group>().HasIndex(i => i.Name).IsUnique();
    }

    #endregion
  }
}
