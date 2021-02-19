namespace Common.DataBase.DataBase.Table
{
  using System.ComponentModel.DataAnnotations;

  public class Band
  {
    #region Properties

    [Key]
    public int Id { get; set; }
    [Required]
    public User User { get; set; }
    [Required]
    public Group Group { get; set; }

    #endregion
  }
}
