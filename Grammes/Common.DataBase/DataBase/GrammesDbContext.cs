namespace Common.DataBase.DataBase
{
  using System.Data.Entity;

  using Table;

  public class GrammesDbContext : DbContext
  {
    #region Properties

    public DbSet<User> Users { get; set; }
    public DbSet<GeneralMessage> GeneralMessages { get; set; }
    public DbSet<PrivateMessage> PrivateMessages { get; set; }
    public DbSet<Event> Events { get; set; }

    #endregion

    #region Constructors

    public GrammesDbContext(string context)
      : base(context)
    {
    }

    #endregion

    #region Methods

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasIndex(i => i.Name).IsUnique();
      modelBuilder.Entity<Group>().HasIndex(i => i.Name).IsUnique();

      modelBuilder.Entity<User>().HasMany(t => t.PrivateMessages)
        .WithRequired(a => a.Target).WillCascadeOnDelete(false);
    }

    #endregion
  }
}
