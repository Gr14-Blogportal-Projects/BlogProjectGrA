using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogProjectGrA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IBlogService _blogService;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, ApplicationDbContext db, IBlogService blogService, IPostService postService)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
            _blogService = blogService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var browseBlogs = _blogService.GetBlogs();
            return View(browseBlogs);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}