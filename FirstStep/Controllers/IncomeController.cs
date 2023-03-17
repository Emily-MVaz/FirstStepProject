using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using FirstStep.Models;

namespace FirstStep.Controllers;

public class IncomeController : Controller
{

  private MyContext _context;

  public IncomeController(MyContext context)
  {
    _context = context;
  }

  //* CREATE INCOME
  [HttpGet("/income")]
  public IActionResult NewIncome()
  {
    return View("income");
  }

  [HttpPost("/income/create")]
  public IActionResult Create(Income income)
  {
    if (!ModelState.IsValid)
    {
      return View("income");
    }
    _context.Incomes.Add(income);
    _context.SaveChanges();
    Console.WriteLine($"{income.IncomeName} has been added!");
    return RedirectToAction("main");
  }


  //* VIEW INCOME
  // public IActionResult ViewIncome()
  // {
  //   List<Income> allIncomes = _context.Incomes.ToList();
  //   return View("viewIncome", allIncomes);
  // }

  //* VIEW INCOME  
  [HttpGet("/viewIncome")]
  public IActionResult ViewDish(int incomeId)
  {
    Income? income = _context.Incomes.FirstOrDefault(i => i.IncomeId == incomeId);
    if (income is null)
    {
      return RedirectToAction("Main", "User");
    }
    return View("ViewIncome", income);
  }

}

