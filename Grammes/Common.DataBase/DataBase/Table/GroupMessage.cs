namespace Common.DataBase.DataBase.Table
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public class GroupMessage
  {
    #region Properties

    [Key]
    public int Id { get; set; }
    [Required]
    public Band Band { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public DateTime Time { get; set; }

    #endregion
  }
}
