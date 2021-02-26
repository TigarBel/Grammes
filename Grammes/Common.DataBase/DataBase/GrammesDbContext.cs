namespace Common.DataBase.DataBase
{
  using System.Data.Entity;

  using Table;

  using DbContext = System.Data.Entity.DbContext;

  public class GrammesDbContext : DbContext
  {
    #region Properties

    public System.Data.Entity.DbSet<User> Users { get; set; }
    public System.Data.Entity.DbSet<GeneralMessage> GeneralMessages { get; set; }
    public System.Data.Entity.DbSet<PrivateMessage> PrivateMessages { get; set; }
    public System.Data.Entity.DbSet<Group> Groups { get; set; }
    public System.Data.Entity.DbSet<Band> Bands { get; set; }
    public System.Data.Entity.DbSet<GroupMessage> GroupMessages { get; set; }
    public System.Data.Entity.DbSet<Event> Events { get; set; }
    

    #endregion

    #region Constructors

    public GrammesDbContext()
      : base("DbConnection")
    {

    }

    #endregion

    #region Methods
    
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasIndex(i => i.Name).IsUnique();
      modelBuilder.Entity<Group>().HasIndex(i => i.Name).IsUnique();
      
      modelBuilder
        .Entity<User>()
        .HasMany(t => t.PrivateMessages)
        .WithRequired(a => a.Target).WillCascadeOnDelete(false);
    }

    #endregion
  }
}
