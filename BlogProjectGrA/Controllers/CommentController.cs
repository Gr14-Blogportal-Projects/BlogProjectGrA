using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;   
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
        public ActionResult Create()
        {
            var comment = new Comment();
            return View(comment);
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
         _commentService.CreateComment(comment);
            return RedirectToAction(nameof(Index));

        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            _commentService.UpdateComment(comment);

            return RedirectToAction(nameof(Index));

        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _commentService.DeleteComment(id);
                return RedirectToAction(nameof(Index));
           
               
            
        }
    }
}
