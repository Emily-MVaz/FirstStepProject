#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstStep.Models;

public class Catgeory
{
  public int CategoryId { get; set; }

  [Required]
  // [Remote("IsUnique", "Home")]
  //*------------------------------------NAME------------------------------------ 
  public string Name { get; set; }
}