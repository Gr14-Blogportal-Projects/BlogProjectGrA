using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Models.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            ViewData["searchBlogResult"] = null;
            ViewData["searchPostsAndTagResult"] = null;
            var sViewModel = new SearchViewModel();

            var searchqueryB = from s in _db.Blogs select s;
            var searchqueryP = from p in _db.Posts select p;
            ViewData["searchB"] = searchqueryB.Where(s => s.Title.Contains(searchparameter));

            if (!string.IsNullOrEmpty(searchparameter))
            {
                sViewModel.Blogs = searchqueryB.Where(s => s.Title.Contains(searchparameter)).OrderByDescending(d => d.CreatedAt).ToList();
                if (sViewModel.Blogs.Any())
                {
                    ViewData["searchBlogResult"] = "1";
                }

                sViewModel.Posts = searchqueryP.Where(p => p.Title.Contains(searchparameter) || p.Tags.FirstOrDefault(s => s.Name == searchparameter) != null).ToList();
                if (sViewModel.Posts.Any())
                {
                    ViewData["searchPostsAndTagResult"] = "1";
                }

                else if (!sViewModel.Blogs.Any() && !sViewModel.Posts.Any())
                {
                    ViewData["NoSearchResult"] = "No result was found.";
                }
            }
            else
            {
                ViewData["NoSearchResult"] = "No result was found.";
            }
            return View(sViewModel);

        }


    }
}
