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
    public int SenderId { get; set; }
    [ForeignKey("SenderId")]
    public User Sender { get; set; }
    public int TargetId { get; set; }
    [ForeignKey("TargetId")]
    public User Target { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public DateTime Time { get; set; }

    #endregion
  }
}
