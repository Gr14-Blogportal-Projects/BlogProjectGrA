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
        private IPostService _postService;
        private readonly ITagService _tagService;
        private readonly IBlogService _blogService;
        private readonly UserManager<User> _userManager;

        public PostController(UserManager<User> userManager, IPostService postService, ITagService tagService, IBlogService blogService)
        {
            _userManager = userManager;
            _postService = postService;
            _tagService = tagService;
            _blogService = blogService;
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
        public ActionResult Create(int id, int blogId, int tagid)
        {
            var tag = _tagService.GetTags(); //new from 30/aug
            var user = _userManager.GetUserAsync(User).Result;

            if (user.Blogs.Count <= 0) 
            {
                TempData["Message"] = "You need to create blog";
                return RedirectToAction("Create", "Blog");
            }
            ViewBag.BlogId = new SelectList(user.Blogs, "Id", "Title" );
            ViewBag.TagId = new SelectList(tag.Select(t => t.Name), "Tags" ); //new from 30/aug
            var post = new Post();
            
            return View(post);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, int blogId, int tagid)
        {
            var tag = _tagService.GetTags(); //new from 30/aug
            var blog = _blogService.GetBlog(blogId);
            post.Blog = blog;
            _postService.CreatePost(post);
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.BlogId = new SelectList(user.Blogs, "Id", "Title");
            ViewBag.TagId = new SelectList(tag.Select(t => t.Name), "Tags"); //new from 30/aug
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "BrowseBlog"); //TODO Redirect to the blog where you make the post
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
