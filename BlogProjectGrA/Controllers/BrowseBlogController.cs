using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogProjectGrA.ViewModels;

namespace BlogProjectGrA.Controllers
{
    public class BrowseBlogController : Controller
    {
        private readonly IBlogService _blogService;

        private readonly UserManager<User> _userManager;

        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public BrowseBlogController(IBlogService blogService, UserManager<User> userManager, IPostService postService ,ITagService tagService)
        {
            _blogService = blogService;
            _userManager = userManager;
            _postService = postService;
            _tagService = tagService;
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
            var posts = _postService.GetPostsByViews();
            return View(posts);
        }
        public ActionResult Tags(int tagId)
        {
            var tags= _tagService.GetTags();

            var selectedTag = tags.FirstOrDefault(t => t.Id == tagId);
            var posts = _postService.GetPostsByTag(selectedTag);
            var vm = new PostsTagsViewModel
            {
                Tags = tags,
                Posts = posts,
                SelectedTag = selectedTag
            };

            return View(vm);
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
