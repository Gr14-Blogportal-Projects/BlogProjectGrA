using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.ViewModels;
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
        public ActionResult Index(string searchparameter)
        {
            ViewData["searchdetails"] = searchparameter;
            var sViewModel = new SearchViewModel();

            var searchqueryB = from s in _db.Blogs select s;
            var searchqueryP = from p in _db.Posts select p;


            //if (searchquery.Any())
            if (!String.IsNullOrEmpty(searchparameter))
            {
                sViewModel.Blogs = searchqueryB.Where(s => s.Title.Contains(searchparameter)).ToList();
                sViewModel.Posts = searchqueryP.Where(p => p.Title.Contains(searchparameter) || p.Tags.FirstOrDefault(s => s.Name == searchparameter) != null).ToList();
            }
            else
            {
                ViewBag.Message = "No result was found";
            }

            return View(sViewModel);
            //return View(await searchquery.AsNoTracking().ToListAsync());

        }


    }
}
