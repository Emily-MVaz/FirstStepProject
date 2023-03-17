using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using FirstStep.Models;

namespace FirstStep.Controllers;

public class ExpenseController : Controller
{

  private MyContext _context;
  public ExpenseController(MyContext context)
  {
    _context = context;
  }

  //* CREATE EXPENSE
  [HttpGet("/expense")]
  public IActionResult NewExpense()
  {
    return View("expense");
  }

  [HttpPost("/expense/create")]
  public IActionResult CreateExpense(Expense expense)
  {
    if (!ModelState.IsValid)
    {
      return View("expense");
    }
    _context.Expenses.Add(expense);
    _context.SaveChanges();
    Console.WriteLine($"{expense.ExpenseName} has been added!");
    return RedirectToAction("main");
  }
  //* VIEW EXPENSE
  [HttpGet("/viewExpenses")]
  public IActionResult ViewDish(int expenseId)
  {
    Expense? expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    if (expense is null)
    {
      return RedirectToAction("Main", "User");
    }
    return View("ViewExpenses", expense);
  }
}

