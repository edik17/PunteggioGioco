using Microsoft.AspNetCore.Mvc;
using TopSecret.Data;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("User");
        if (string.IsNullOrEmpty(username))
            return RedirectToAction("Register", "Account");

        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        return View(user);
    }

    [HttpPost]
    public IActionResult AddPoint()
    {
        var username = HttpContext.Session.GetString("User");
        if (string.IsNullOrEmpty(username))
            return RedirectToAction("Login", "Account");

        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            user.Points += 1;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
