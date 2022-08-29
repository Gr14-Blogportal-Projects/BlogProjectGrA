using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Models.ViewModels;

using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class PostController : Controller
    {


        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;
        private readonly IBlogService _blogService;
    
        public PostController(IPostService postService, IBlogService blogService,UserManager<User> userManeger)

        {
           
            _postService = postService;
            _blogService = blogService;
            _userManager = userManeger;
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
        public ActionResult Create(int id, int blogId)
        {
            
            var user = _userManager.GetUserAsync(User).Result;

            if (user.Blogs.Count <= 0) 
            {
                TempData["Message"] = "You need to create blog";
                return RedirectToAction("Create", "Blog");
            }
            ViewBag.BlogId = new SelectList(user.Blogs, "Id", "Title" );
          
            //var title = _db.Blogs.Select(x => new System.Web.Mvc.SelectListItem()
            //{
            //    Text = x.Title,
            //    Value = x.Id.ToString()
            //}).ToList();
            //CreatePostVM vm = new CreatePostVM();
            //vm.Blogs = title;
            var post = new Post();

            return View(post);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, int blogId)
        {
            var blog = _blogService.GetBlog(blogId);
            post.Blog = blog;
            _postService.CreatePost(post);
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.BlogId = new SelectList(user.Blogs, "Id", "Title");

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
