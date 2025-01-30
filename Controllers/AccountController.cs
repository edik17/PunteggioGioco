using Microsoft.AspNetCore.Mvc;
using TopSecret.Data;
using TopSecret.Models;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string pin)
    {
        if (_context.Users.Any(u => u.Username == username))
        {
            ViewBag.Message = "Username già in uso.";
            return View();
        }

        var user = new User { Username = username, Pin = pin, Points = 0 };
        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string pin)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Pin == pin);
        if (user != null)
        {
            HttpContext.Session.SetString("User", user.Username);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Credenziali errate.";
        return View();
    }

}
