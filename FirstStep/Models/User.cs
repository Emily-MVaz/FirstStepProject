#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstStep.Models;

public class User
{
  [Key]
  public int UserId { get; set; }

  //*------------------------------------FIRST NAME------------------------------------ 
  [Display(Name = "First Name")]
  [MinLength(2, ErrorMessage = "First name must be at least 2 characters!")]
  [Required]
  public string FirstName { get; set; }

  //*------------------------------------LAST NAME------------------------------------
  [Display(Name = "Last Name")]
  [MinLength(2, ErrorMessage = "Last name must be at least 2 characters!")]
  [Required]
  public string LastName { get; set; }

  //*------------------------------------EMAIL------------------------------------
  [EmailAddress]
  [Required]
  [UniqueEmail]
  public string Email { get; set; }

  //*------------------------------------PASSWORD------------------------------------
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters!")]
  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  //*------------------------------------CONFIRM------------------------------------
  [NotMapped]
  [DataType(DataType.Password)]
  [Compare("Password")]
  [Required]
  [Display(Name = "Confirm Password")]
  public string PasswordConfirm { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
//*------------------------------------UNIQUE EMAIL------------------------------------
public class UniqueEmailAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value == null)
    {
      return new ValidationResult("Email is required!");
    }

    MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
    if (_context.Users.Any(e => e.Email == value.ToString()))
    {
      return new ValidationResult("Email is already in use!");
    }
    else
    {
      return ValidationResult.Success;
    }
  }
}
