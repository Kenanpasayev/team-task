using Microsoft.AspNetCore.Mvc;
using WebApplication14.DAL;
using WebApplication14.Models;

namespace WebApplication14.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid) return View();
            string path = _environment.WebRootPath + @"\Upload\";
            string filename = Guid.NewGuid() + team.formFile.FileName;
            using (FileStream fileStream = new FileStream(path + filename, FileMode.Create))
            {
                team.formFile.CopyTo(fileStream);

            }
            team.ImgUrl = filename;
            _context.Teams.Add(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {

            Team teams = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (teams == null)
            {
                return RedirectToAction("Index");
            }
            return View(teams);
        }
        [HttpPost]
        public IActionResult Update(Team teams)
        {
            Team oldTeams = _context.Teams.FirstOrDefault(x => x.Id == teams.Id);
            if (oldTeams == null) return NotFound();
            if (teams.formFile != null)
            {
                string path = _environment.WebRootPath + @"\Upload\";
                string filname = Guid.NewGuid() + teams.formFile.FileName;
                using (FileStream stream = new FileStream(path + filname, FileMode.Create))
                {
                    teams.formFile.CopyTo(stream);
                }
                oldTeams.ImgUrl = filname;
            }

            oldTeams.FullName = teams.FullName;
            oldTeams.Position = teams.Position;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var team = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (team != null)
            {
                string path = _environment.WebRootPath + @"\Upload\" + team.formFile;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                _context.Teams.Remove(team);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
