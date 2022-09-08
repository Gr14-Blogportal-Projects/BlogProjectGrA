﻿using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public BlogController(IBlogService blogService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _blogService = blogService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

      
        // GET: BlogController
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var blog = _blogService.GetBlogsByUser(userId);
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
            var blog = new Blog();
            return View(blog);
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            var user = _userManager.GetUserAsync(User).Result;
            blog.Author = user;
            _blogService.CreateBlog(blog);
            return RedirectToAction(nameof(Index));
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id, Blog blog)
        {
            if (_userManager.GetUserId(User) == _blogService.GetBlog(id).Author.Id) 
            {
                var getBlog = _blogService.GetBlog(id);
                return View(getBlog);
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
        public ActionResult Edit(Blog blog)
        {
            _blogService.UpdateBlog(blog);
            return RedirectToAction("Details", "BrowseBlog", blog);
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_userManager.GetUserId(User) == _blogService.GetBlog(id).Author.Id)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var blog = _blogService.GetBlog(id);
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

        public ActionResult Delete(int id,Blog blog)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _blogService.DeleteBlog(blog);
            return RedirectToAction(nameof(Index));
        }
    }
}
