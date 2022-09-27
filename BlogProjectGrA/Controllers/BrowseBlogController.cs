using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogProjectGrA.Models.ViewModels;
using X.PagedList ;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

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
        public ActionResult Index(int id, int views,int?page)
        {
            

            var post = _postService.GetPost(id);
            var blog = _blogService.GetBlog(id);
            var browseBlogs = _blogService.GetBlogs().OrderByDescending(b => b.CreatedAt).ToPagedList(page ?? 1,3);
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

        public ActionResult Details(int id, int year, int month, int? page)
        {
            var blog = _blogService.GetBlog(id);
            List<Post> posts;
            if (year != 0 || month != 0)
            {
                posts = _postService.GetPostByDate(id, year, month).ToList();
            }
            else
            {
                posts = blog.Posts.ToList();
            }
            var allDates = blog.Posts.OrderByDescending(p=>p.CreateAt).ToList().Select(p => p.CreateAt).ToList().ToPagedList(page ?? 1, 3);

            var dates = new List<DatesCountVM>();
            foreach (var item in allDates)
            {
                var existing = dates.FirstOrDefault(d => d.Date.Year == item.Year && d.Date.Month == item.Month);
                if (existing == null)
                {
                    var count = allDates.Where(d => d.Date.Year == item.Year && d.Date.Month == item.Month).Count();
                    dates.Add(new DatesCountVM { Count = count, Date = item});
                }
            }

            var dateByPostVm = new DateByPostVM()
            {
                Blog = blog,
                Posts = posts,
                Dates = dates,
                Year = year,    
                Month=month
            };

            return View(dateByPostVm);
            //var blog = _blogService.GetBlog(id);
            //return View(blog);
        }

        public ActionResult Blog(string id, int? page) //View not existing
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
