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

  //* VIEW SINGLE EXPENSE
  [HttpGet("/expenses/{expenseId}")]
  public IActionResult ViewExpense(int expenseId)
  {
    Expense? expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    if (expense is null)
    {
      return RedirectToAction("Main", "User");
    }
    return View("ViewExpenses", expense);
  }

  //* EDIT EXPENSE
  [HttpGet("/expenses/{expenseId}/edit")]
  public IActionResult Edit(int expenseId)
  {
    Expense? expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    if (expense is null)
    {
      return RedirectToAction("Main", "User");
    }
    return View("editExpense", expense);
  }

  [HttpPost("/expense/{expenseId}/update")]
  public IActionResult Update(Expense updatedExpense, int expenseId)
  {
    if (!ModelState.IsValid)
    {
      Console.WriteLine("validations failed :( ");
      return Edit(expenseId);
    }

    Expense? expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    // Expense? expense = _context._context.Entry(expense).State = EntityState.Modified;
    if (expense is null)
    {
      return RedirectToAction("Main", "User");
    }

    expense.ExpenseName = updatedExpense.ExpenseName;
    expense.Amount = updatedExpense.Amount;
    expense.Category = updatedExpense.Category;
    expense.date_time = updatedExpense.date_time;
    expense.Description = updatedExpense.Description;
    expense.UpdatedAt = DateTime.Now;

    _context.Expenses.Update(expense);
    _context.SaveChanges();

    return RedirectToAction("ViewExpense", new { expenseId = expenseId });
  }

  //* DELETE EXPENSE 
  [HttpGet("/expense/{expenseId}/delete")]
  public IActionResult Delete(int expenseId)
  {
    Expense? expense = _context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
    if (expense != null)
    {
      _context.Expenses.Remove(expense);
      _context.SaveChanges();
    }

    return RedirectToAction("Main", "User");
  }
}

