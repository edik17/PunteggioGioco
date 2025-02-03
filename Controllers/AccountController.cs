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
        if (string.IsNullOrWhiteSpace(username) || username.Length > 50)
        {
            ViewBag.Message = "Nome utente non valido (max 50 caratteri).";
            return View();
        }

        username = username.Trim();

        if (!System.Text.RegularExpressions.Regex.IsMatch(username, "^[a-zA-Z0-9]+$"))
        {
            ViewBag.Message = "Il nome utente può contenere solo lettere e numeri.";
            return View();
        }

        if (!System.Text.RegularExpressions.Regex.IsMatch(pin, "^[a-zA-Z0-9]{4,10}$"))
        {
            ViewBag.Message = "Il PIN deve contenere solo lettere e numeri e deve essere lungo tra 4 e 10 caratteri.";
            return View();
        }

        if (_context.Users.Any(u => u.Username == username))
        {
            ViewBag.Message = "Username già in uso.";
            return View();
        }

        var user = new User
        {
            Username = username,
            Pin = pin,
            Points = 0,
            CreatedAt = DateTime.UtcNow
        };

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

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

}
