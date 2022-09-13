using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, UserManager<User> userManager, IPostService postService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _postService = postService;
        }
        [AllowAnonymous]
        // GET: CommentController
        public ActionResult Index()
        {
            var comments = _commentService.GetComments();
            return View(comments);
        }
        [AllowAnonymous]
        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            var comment = _commentService.GetCommentsByPost(id);
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            //ViewBag.PostId = new Comment(user.Posts)
            var comment = new Comment();
            return View(comment);
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id ,string commentBody)
        {
            var comment = new Comment();
            var post = _postService.GetPost(id);
            var user = _userManager.GetUserAsync(User).Result;
            comment.Body= commentBody;
            comment.Author = user;
            comment.Posts = post;
            _commentService.CreateComment(comment);

            return RedirectToAction("Details", "Post", new { id });//nameof(Index)

        }




        // GET: CommentController/Edit/5
        public ActionResult Edit(int id,Comment comment)
        {
            var comId = _commentService.GetComment(id);
            if (comId == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) == comId.Author.Id)
            {

                return View(comId);
            }
            else
            {
                return NotFound("Denied access.");

            }
            
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            
            _commentService.UpdateComment(comment);
            //var commentUpdated = _commentService.UpdateComment(comment);
            return RedirectToAction("Details", "Post", new { id=comment.PostsId });

        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            var comment = _commentService.GetComment(id);
            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            _commentService.DeleteComment(comment);
                return RedirectToAction("Details", "Post", new { id = comment.PostsId });
           
               
            
        }
    }
}
