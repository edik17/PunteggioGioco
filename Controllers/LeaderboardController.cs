using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TopSecret.Data;
using TopSecret.Models;

namespace TopSecret.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly AppDbContext _context;

        public LeaderboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var sortedUsers = _context.Users.OrderByDescending(u => u.Points).ToList();
            return View(sortedUsers);
        }
    }
}
