using Microsoft.AspNetCore.Mvc;
using WebApplication14.DAL;

namespace WebApplication14.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Teams.ToList());
        }

    }
}