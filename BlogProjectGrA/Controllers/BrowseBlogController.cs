using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    public class BrowseBlogController : Controller
    {
        private readonly IBlogService _blogService;

        private readonly UserManager<User> _userManager;

        private readonly IPostService _postService;

        public BrowseBlogController(IBlogService blogService, UserManager<User> userManager, IPostService postService)
        {
            _blogService = blogService;
            _userManager = userManager;
            _postService = postService;
        }
        // GET: BrowseBlogController
        public ActionResult Index(int id)
        {
            //var user = _userManager.GetUserId(User);
            var browseBlogs = _blogService.GetBlogs();
            return View(browseBlogs);
        }

        // GET: BrowseBlogController/Details/5
        public ActionResult Views()
        {
            var browseblog = _blogService.GetBlogs();
            //var blogview = _postService.GetPostByViews(id, view);
            //var blogview = _blogService.GetBlogs().OrderByDescending(p => p.);
            
            return View(browseblog);
        }
        public ActionResult Tags(int id)
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var blog = _blogService.GetBlog(id);
            return View(blog);
        }

        public ActionResult Blog(string id)
        {
            var blog = _blogService.GetBlogsByUser(id);
            return View(blog);
        }

        // GET: BrowseBlogController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BrowseBlogController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
