using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        // GET: BlogController
        public ActionResult Index()
        {
            var blogs = _blogService.GetBlogs();
            return View(blogs);
        }

        [AllowAnonymous]
        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            var blog = _blogService.GetBlog(id);
            return View(blog);
        }

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
            //var user =
            //blog.Author = user;
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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _blogService.DeleteBlog(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
