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
        private readonly ICommentService _commentService;
        private readonly SignInManager<User> _signInManager;

        public PostController(UserManager<User> userManager, IPostService postService, ITagService tagService, IBlogService blogService, ICommentService commentService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _postService = postService;
            _tagService = tagService;
            _blogService = blogService;
            _commentService = commentService;
            _signInManager = signInManager;

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
            var tag = _tagService.GetTags();
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
        public ActionResult Create(Post post, int blogId, string tagListString)
        {
            var tagList = tagListString.Split(',');

            var tags = _tagService.GetOrCreateTags(tagList);

            var blog = _blogService.GetBlog(blogId);
            post.Blog = blog;

            post.Tags = tags.ToList();

            _postService.CreatePost(post);
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.BlogId = new SelectList(user.Blogs, "Id", "Title");
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "BrowseBlog"); //TODO Redirect to the blog where you make the post
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) == post.Blog.Author.Id)
            {

                return View(post);
            }
            else
            {
                return NotFound("Denied access.");

            }
            
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post, int blogId)
        {
            var blog = _blogService.GetBlog(blogId);
            post.Blog=blog;
            _postService.UpdatePost(post);
            return RedirectToAction("Posts","Blog", new { id = post.Blog.Id });
            //return RedirectToAction("Posts", "Blog", blog.Posts);


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
