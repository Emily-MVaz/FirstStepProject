#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstStep.Models;

public class Income
{

  [Key]
  public int IncomeId { get; set; }
  //*------------------------------------NAME------------------------------------ 
  [Required(ErrorMessage = "Please enter a name!")]
  public string IncomeName { get; set; }
  //*------------------------------------AMOUNT------------------------------------
  [Required]
  [Range(0, Int32.MaxValue, ErrorMessage = "Amount must be greater than 0")]
  public int Amount { get; set; }
  //*------------------------------------CATEGORY------------------------------------
  [Required(ErrorMessage = "Please choose a category!")]
  public string Category { get; set; }
  //*------------------------------------DATE------------------------------------
  [Required]
  [FutureDate]
  public DateTime date_time { get; set; }
  //*------------------------------------DESCRIPTION------------------------------------
  [Required(ErrorMessage = "Please enter a description!")]
  public string Description { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

