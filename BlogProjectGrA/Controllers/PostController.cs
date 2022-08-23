using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    public class PostController : Controller
    {
        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        // GET: HomeController1
        public ActionResult Index()
        {
            var posts = _postService.GetPosts();
            return View(posts);
            
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var updatedPost = _postService.IncrementViews(id);
            return View(updatedPost);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            var post = new Post();
            return View(post);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            _postService.CreatePost(post);

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            var post = _postService.GetPostsByBlog(id);
            return View(post);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post)
        {
            _postService.UpdatePost(post);
            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            var post = _postService.GetPostsByBlog(id);
            return View(post);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Post post)
        {
            _postService.DeletePost(id);

            return RedirectToAction(nameof(Index));
        }

        
    }
}
