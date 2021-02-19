namespace Common.DataBase.DataBase.Table
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class PrivateMessage
  {
    #region Properties

    [Key]
    public int Id { get; set; }
    [Required]
    [ForeignKey("Sender")]
    public int SenderId { get; set; }
    public User Sender { get; set; }
    [Required]
    [ForeignKey("Target")]
    public int TargetId { get; set; }
    public User Target { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public DateTime Time { get; set; }

    #endregion
  }
}
