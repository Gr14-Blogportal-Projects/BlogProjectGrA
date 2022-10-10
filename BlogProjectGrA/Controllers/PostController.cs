using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using BlogProjectGrA.Models.ViewModels;
using X.PagedList;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

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
        public ActionResult Index(int? page)
        {
            //var posts = _postService.GetPosts().ToPagedList(page ?? 1, 3);
            return RedirectToAction("Posts","Blog");
            
        }
        [AllowAnonymous]
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var updatedPost = _postService.IncrementViews(id);
            return View(updatedPost);
        }

        // GET: HomeController1/Create
        public ActionResult Create(int blogId)
        {
            TempData["PostMessage"] = null;
            var tag = _tagService.GetTags();
            var user = _userManager.GetUserAsync(User).Result;

            if (user.Blogs.Count <= 0) 
            {
                TempData["Message"] = "You need to create blog";
                return RedirectToAction("Create", "Blog");
            }
            var blog = user.Blogs.FirstOrDefault(b => b.Id == blogId);
            if (blog == null)
            {
                return NotFound();
            }
            
            ViewData["selectedBlogId"] = blogId;
            //ViewBag.BlogId = new SelectList(user.Blogs, "Id", "Title" );
            ViewBag.TagId = new SelectList(tag.Select(t => t.Name), "Tags" ); //new from 30/aug
            var post = new CreatePostVM
            {
                Post = new()
                {
                    Blog = blog,
                }
            };
            return View(post);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostVM vm, int blogId, string tagListString)
        {
            var tagList = tagListString.Split(',');

            var tags = _tagService.GetOrCreateTags(tagList);
            ViewData["selectedBlogId"] = blogId;
            var blog = _blogService.GetBlog(blogId);
            vm.Post.Blog = blog;

            vm.Post.Tags = tags.ToList();

            var createdPost = _postService.CreatePost(vm.Post);
            TempData["PostMessage"] = "Your Post has been made.";

            if (vm.Files != null)
            {
                var databaseFiles = new List<PostImage>();
                foreach (var file in vm.Files)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Posts");
                    // create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    databaseFiles.Add(new()
                    {
                        Url = "Images/Posts/" + fileName,
                        Post = createdPost
                    });
                }
                _postService.CreateImages(databaseFiles);
            }
            return RedirectToAction("Details", "BrowseBlog", new { id = blog.Id }); //TODO Redirect to the blog where you make the post



        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            TempData["EditPostMessage"] = null;
            
            var post = _postService.GetPost(id);
           
            if (post == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) == post.Blog.Author.Id)
            {

                var vm = new CreatePostVM();
                
                vm.Post = post;
                if (vm.Files == null)
                {
                    vm.Post.Images = post.Images;
                }
                
                return View(vm);
            }
                  
            else
            {
                       
                return NotFound("Denied access.");
                    
            }
                

            


            
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreatePostVM vm, int id, Post post, string tagsString,PostImage postImage)
        {
            var tagList = tagsString.Split(',');
            var tags = _tagService.GetOrCreateTags(tagList);
           
            var existingPost = _postService.GetPost(vm.Post.Id);
            
            //var updatePost = _postService.UpdatePost(post);
            existingPost = _postService.RemovePostTags(existingPost);

            // existingPost.Images= post.Images.ToList();
           // existingPost = vm.Post;
            existingPost.Title = post.Title;
            existingPost.Body = post.Body;
            existingPost.Tags = tags.ToList();

            if (vm.Files != null)
            {
                
               // existingPost.Images = postImage.Url;
                var databaseFiles = new List<PostImage>();
                foreach (var file in vm.Files)
                {

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Posts");
                    // create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    databaseFiles.Add(new()
                    {
                        Url = "Images/Posts/" + fileName,
                        Post = vm.Post
                    });
                }
                
            }
           


            _postService.UpdatePost(existingPost);
           
            
            TempData["EditPostMessage"] = "Your Post has been updated.";
            return RedirectToAction("Details","Post", new { id = existingPost.Id });
            //return RedirectToAction("Posts", "Blog", blog.Posts);
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            TempData["DeletePostMessage"] = null;
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

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, Post post)
        {
            _postService.DeletePost(id);
            TempData["DeletePostMessage"] = "Your Post has been deleted.";
            return RedirectToAction("Index", "Blog");
            //return RedirectToAction(nameof(Index));
        }

        
    }
}
