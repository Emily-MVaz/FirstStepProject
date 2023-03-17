using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using FirstStep.Models;

namespace FirstStep.Controllers;

public class UserController : Controller
{

  private MyContext _context;

  public UserController(MyContext context)
  {
    _context = context;
  }


  //* -----------------------------INDEX----------------------------- 
  [HttpGet("")]
  public IActionResult Index()
  {
    return View("Index");
  }

  //* -----------------------------SESSION----------------------------- 
  [SessionCheck]
  [HttpGet("main")]
  public IActionResult Main()
  {
    return View("Main");
  }

  //* -----------------------------REGISTER----------------------------- 
  [HttpPost("/register")]
  public IActionResult Register(User user)
  {
    if (!ModelState.IsValid)
    {
      return View("Index");
    }
    PasswordHasher<User> Hasher = new PasswordHasher<User>();
    user.Password = Hasher.HashPassword(user, user.Password);

    _context.Users.Add(user);
    _context.SaveChanges();
    HttpContext.Session.SetInt32("UserId", user.UserId);
    return RedirectToAction("main");
  }

  //* -----------------------------LOGIN----------------------------- 
  [HttpPost("/login")]
  public IActionResult Login(LoginUser getUser)
  {
    if (!ModelState.IsValid)
    {
      return View("Index");
    }
    else
    {
      User? userInDb = _context.Users.FirstOrDefault(u => u.Email == getUser.LoginEmail);
      if (userInDb == null)
      {
        ModelState.AddModelError("LoginEmail", "Invalid Email");
        return View("Index");
      }
      else
      {
        PasswordHasher<LoginUser> hash = new PasswordHasher<LoginUser>();
        var result = hash.VerifyHashedPassword(getUser, userInDb.Password, getUser.LoginPassword);
        if (result == 0)
        {
          ModelState.AddModelError("LoginPassword", "Invalid Password");
          return View("Index");
        }
        else
        {
          HttpContext.Session.SetInt32("UserId", userInDb.UserId);
          HttpContext.Session.SetString("FirstName", userInDb.FirstName);
          return RedirectToAction("main");
        }
      }
    }
  }

  //* -----------------------------LOGOUT----------------------------- 
  [HttpPost("/logout")]
  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return RedirectToAction("Index");
  }

  //* -----------------------------SESSION CHECK----------------------------- 
  public class SessionCheckAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      int? userId = context.HttpContext.Session.GetInt32("UserId");
      if (userId == null)
      {
        context.Result = new RedirectToActionResult("Index", "User", null);
      }
    }
  }

  // public IActionResult Main2()
  // {
  //   List<Transaction> getTransactions = _context.Transactions.ToList();
  //   return View("Main", getTransactions);
  // }
}