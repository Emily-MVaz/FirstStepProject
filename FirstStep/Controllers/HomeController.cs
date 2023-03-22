using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstStep.Models;

namespace FirstStep.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private MyContext _context;

  public HomeController(ILogger<HomeController> logger, MyContext context)
  {
    _logger = logger;
    _context = context;
  }

  public IActionResult Index()
  {
    return View();
  }
  public IActionResult ListIncome()
  {
    List<Income> allIncomes = _context.Incomes.ToList();
    return View("ListIncome", allIncomes);
  }
  public IActionResult ListExpenses()
  {
    List<Expense> allExpenses = _context.Expenses.ToList();
    return View("ListExpenses", allExpenses);
  }


  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
