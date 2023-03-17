#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstStep.Models;

[NotMapped]
public class LoginUser
{
  //*-------------------EMAIL------------------- 
  [Required]
  [EmailAddress]
  [Display(Name = "Email")]
  public string LoginEmail { get; set; }

  //*-------------------PASSWORD-------------------
  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Password")]
  public string LoginPassword { get; set; }
}