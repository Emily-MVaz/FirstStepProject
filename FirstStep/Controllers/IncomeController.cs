using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using FirstStep.Models;

namespace FirstStep.Controllers;

public class IncomeController : Controller
{
  private readonly ILogger<IncomeController> _logger;
  private MyContext _context;

  public IncomeController(ILogger<IncomeController> logger, MyContext context)
  {
    _logger = logger;
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
    return RedirectToAction("viewIncome", "Incomes");
  }


  //* VIEW SINGLE INCOME  
  [HttpGet("/incomes/{incomeId}")]
  public IActionResult ViewIncome(int incomeId)
  {
    Income? income = _context.Incomes.FirstOrDefault(i => i.IncomeId == incomeId);
    if (income is null)
    {
      return RedirectToAction("Main", "User");
    }
    return View("viewIncome", income);
  }

  //* EDIT INCOME
  [HttpGet("/incomes/{incomeId}/edit")]
  public IActionResult Edit(int incomeId)
  {
    Income? income = _context.Incomes.FirstOrDefault(d => d.IncomeId == incomeId);
    if (income is null)
    {
      return RedirectToAction("Main", "User");
    }
    return View("editIncome", income);
  }

  [HttpPost("/incomes/{incomeId}/update")]
  public IActionResult Update(Income updatedIncome, int incomeId)
  {
    if (!ModelState.IsValid)
    {
      Console.WriteLine("validations failed :( ");
      return Edit(incomeId);
    }

    Income? income = _context.Incomes.FirstOrDefault(d => d.IncomeId == incomeId);
    if (income is null)
    {
      return RedirectToAction("Main", "User");
    }

    income.IncomeName = updatedIncome.IncomeName;
    income.Amount = updatedIncome.Amount;
    income.Category = updatedIncome.Category;
    income.date_time = updatedIncome.date_time;
    income.Description = updatedIncome.Description;
    income.UpdatedAt = DateTime.Now;

    _context.Incomes.Update(income);
    _context.SaveChanges();

    return RedirectToAction("ViewIncome", new { incomeId = incomeId });
  }

  //* DELETE INCOME 
  [HttpGet("/income/{incomeId}/delete")]
  public IActionResult Delete(int incomeId)
  {
    Income? income = _context.Incomes.FirstOrDefault(d => d.IncomeId == incomeId);
    if (income != null)
    {
      _context.Incomes.Remove(income);
      _context.SaveChanges();
    }

    return RedirectToAction("Main", "User");
  }

}

