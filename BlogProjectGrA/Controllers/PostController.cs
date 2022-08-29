using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Models.ViewModels;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private IPostService _postService;
        private readonly ApplicationDbContext _db;
        public PostController(IPostService postService, ApplicationDbContext db)
        {
            _db=db; 
            _postService = postService;
        }
        [AllowAnonymous]
        // GET: HomeController1
        public ActionResult Index()
        {
            var posts = _postService.GetPosts();
            return View(posts);
            
        }
        [AllowAnonymous]
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var updatedPost = _postService.IncrementViews(id);
            return View(updatedPost);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            var post= new Post();
            ViewBag.BlogId = new SelectList(_db.Blogs, "Id", "Title");
          
            //var title = _db.Blogs.Select(x => new System.Web.Mvc.SelectListItem()
            //{
            //    Text = x.Title,
            //    Value = x.Id.ToString()
            //}).ToList();
            //CreatePostVM vm = new CreatePostVM();
            //vm.Blogs = title;
            return View(post);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post ,int id)
        {
          

            _postService.CreatePost(post);
           ViewBag.BlogId = new SelectList(_db.Blogs, "Id", "Title",id);

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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _postService.DeletePost(id);

            return RedirectToAction(nameof(Index));
        }

        
    }
}
