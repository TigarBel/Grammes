namespace Common.DataBase.DataBase.Table
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class GeneralMessage
  {
    #region Properties

    [Key]
    public int Id { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public DateTime Time { get; set; }
    public int User_Id { get; set; }
    [ForeignKey("User_Id")]
    public User User { get; set; }

    #endregion
  }
}
