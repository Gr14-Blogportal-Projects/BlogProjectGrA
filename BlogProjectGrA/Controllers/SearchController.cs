using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectGrA.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public SearchController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }
        // GET: SearchController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchparameter)
        {
            ViewData["searchdetails"] = searchparameter;

            var searchquery = from s in _db.Blogs select s;
            //if (searchquery.Any())
            if (!String.IsNullOrEmpty(searchparameter))
            {
                searchquery = searchquery.Where(s => s.Title.Contains(searchparameter));
            }
            else
            {
                ViewBag.Message = "No result was found";
            }
            return View(await searchquery.ToListAsync());
            //return View(await searchquery.AsNoTracking().ToListAsync());
        }

    }
}
