using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        private readonly UserManager<User> _userManager;

        public BlogController(IBlogService blogService, UserManager<User> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
        }

      
        // GET: BlogController
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var blog = _blogService.GetBlogsByUser(userId);
             return View(blog);
        }

        //[AllowAnonymous]
        //// GET: BlogController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var blog = _blogService.GetBlog(id);
        //    return View(blog);
        //}

        // GET: BlogController/Create
        public ActionResult Create()
        {
            var blog = new Blog();
            return View(blog);
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            var user = _userManager.GetUserAsync(User).Result;
            blog.Author = user;
            _blogService.CreateBlog(blog);
            return RedirectToAction(nameof(Index));
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog)
        {
            _blogService.UpdateBlog(blog);
            return RedirectToAction(nameof(Index));
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            var blog = _blogService.GetBlog(id);
            return View(blog);
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id,Blog blog)
        {
            _blogService.DeleteBlog(blog);
            return RedirectToAction(nameof(Index));
        }
    }
}
