using BlogProjectGrA.Models;

using BlogProjectGrA.Models.ViewModels;
using BlogProjectGrA.Models.Email;

using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;
        private readonly HttpClient  _httpClient;

      public BlogController(IBlogService blogService, UserManager<User> userManager, SignInManager<User> signInManager, IHttpClientFactory httpClientFactory)
        {
            _blogService = blogService;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = httpClientFactory.CreateClient("BlogEmailconformation");
        }


        // GET: BlogController
        public ActionResult Index( int? page)
        {
            TempData["SuccessMessage"] = null;
            TempData["PostMessage"] = null;
            TempData["EditBlogMessage"] = null;
            TempData["DeleteBlogMessage"] = null;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var blog = _blogService.GetBlogsByUser(userId).OrderByDescending(d => d.CreatedAt).ToPagedList(page ?? 1, 3);
            
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
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create(CreateBlogVM vm)
        {
            if(vm.File != null)
            { 
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blogs");
            // create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string fileName = Guid.NewGuid().ToString() + "_" + vm.File.FileName;
            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                vm.File.CopyTo(stream);
            }
                vm.Blog.ImageUrl = "Images/Blogs/" + fileName;
            }
            var user = _userManager.GetUserAsync(User).Result;
            vm.Blog.Author = user;
            
            _blogService.CreateBlog(vm.Blog);
            // Send email here
            var email = new CreateEmail
            {
                Author = vm.Blog.Author.GetName(),
                Email = vm.Blog.Author.Email,
                BlogTitle = vm.Blog.Title,
                Date = vm.Blog.CreatedAt
            };
            var res = await _httpClient.PostAsJsonAsync("SendConfirmationEmail", email);
            TempData["SuccessMessage"] = res.IsSuccessStatusCode ? "Successfully sent Email" : "Failed to send Email";

            
            return RedirectToAction("Details", "BrowseBlog", new {id = vm.Blog.Id});
         }


        // GET: BlogController/Edit/5
        //
        public ActionResult Edit(int id)
        {
            TempData["EditBlogMessage"] = null;
            var gblog = _blogService.GetBlog(id);
            if (gblog == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) == gblog.Author.Id) 
            {
                var vm= new CreateBlogVM();
                vm.Blog = gblog;
                return View(vm);
            }
            else
            {
                return NotFound("Denied access.");
                //return NoContent();
            }
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateBlogVM vm)
        {
            if (vm.Blog == null)
            {
                return NotFound();
            }
            if (vm.File != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                //    //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileName = Guid.NewGuid().ToString() + "_" + vm.File.FileName;
                string fileNameWithPath = Path.Combine(path, fileName);
               
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    vm.File.CopyTo(stream);
                    
                }
                vm.Blog.ImageUrl = "Images/" + fileName;
            }
            TempData["EditBlogMessage"] = "Blog has been updated.";
            _blogService.UpdateBlog(vm.Blog);
            return RedirectToAction("Details", "BrowseBlog", new { id=vm.Blog.Id });
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            TempData["DeleteBlogMessage"] = null;
            var blog = _blogService.GetBlog(id);
            if (blog == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) == blog.Author.Id)
            {
                return View(blog);
            }   
            else
            {
                return NotFound("Denied access.");
            }
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Blog blog)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            _blogService.DeleteBlog(id);
            TempData["DeleteBlogMessage"] = "Blog has been deleted.";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Posts (int id, int? page)
        {
            
            var blog = _blogService.GetBlog(id);
            //if (blog == null)
            //{
            //    return NotFound();
            //}
            //if (_userManager.GetUserId(User) == blog.Author.Id)
            //{

            //    return View(blog);
            //}
            //else
            //{
            //    return NotFound("Denied access.");
                
            //}
            return View(blog);
        }
    }
}
