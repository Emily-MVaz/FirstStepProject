#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstStep.Models;

public class Expense
{

  [Key]
  public int ExpenseId { get; set; }
  //*------------------------------------NAME------------------------------------ 
  [Required(ErrorMessage = "Please enter a name!")]
  public string ExpenseName { get; set; }
  //*------------------------------------AMOUNT------------------------------------
  [Required]
  [DataType(DataType.Currency)]
  [Range(0, Int32.MaxValue, ErrorMessage = "Amount must be greater than 0")]
  public int Amount { get; set; }
  //*------------------------------------CATEGORY------------------------------------
  [Required(ErrorMessage = "Please choose a category!")]
  public string Category { get; set; }
  //*------------------------------------DATE------------------------------------
  [Required]
  [FutureDate]
  [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
  public DateTime date_time { get; set; }
  //*------------------------------------DESCRIPTION------------------------------------
  [Required(ErrorMessage = "Please enter a description!")]
  public string Description { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
public class FutureDateAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    TimeSpan diff = DateTime.Now - (DateTime)value;
    if (diff < TimeSpan.Zero)
    {
      return new ValidationResult("Please choose a date earlier than the current date.");
    }
    return ValidationResult.Success;
  }
}