#pragma warning disable CS8618

namespace FirstStep.Models;

public class MyViewModel
{
  public List<Income> AllIncomes { get; set; } = new List<Income>();
  public Income Income { get; set; }
  public List<Expense> AllExpenses { get; set; } = new List<Expense>();
  public Expense Expense { get; set; }


}