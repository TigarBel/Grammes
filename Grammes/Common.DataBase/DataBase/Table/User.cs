namespace Common.DataBase.DataBase.Table
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class User
  {
    #region Properties

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(16)]
    public string Name { get; set; }
    public List<GeneralMessage> GeneralMessages { get; set; }
    public List<PrivateMessage> PrivateMessages { get; set; }
    public List<Band> Bands { get; set; }

    #endregion
  }
}
