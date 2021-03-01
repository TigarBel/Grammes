namespace Common.DataBase.DataBase.Table
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  using DataBaseAndNetwork.EventLog;

  public class Event
  {
    #region Properties

    [Key]
    public int Id { get; set; }

    [Required]
    public bool IsSuccessfully { get; set; }

    [Required]
    public DispatchType Type { get; set; }

    [Required]
    public string Message { get; set; }

    [Required]
    public DateTime Time { get; set; }

    [Required]
    public string UserName { get; set; }
    
    #endregion
  }
}
