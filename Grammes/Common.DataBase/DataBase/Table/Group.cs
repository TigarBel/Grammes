namespace Common.DataBase.DataBase.Table
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Group
  {
    #region Properties

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(16)]
    public string Name { get; set; }
    public List<Band> Bands { get; set; }

    #endregion
  }
}
