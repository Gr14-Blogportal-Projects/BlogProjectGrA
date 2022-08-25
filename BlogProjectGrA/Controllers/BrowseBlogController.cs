﻿using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    public class BrowseBlogController : Controller
    {
        private readonly IBlogService _blogService;

        private readonly UserManager<User> _userManager;
        public BrowseBlogController(IBlogService blogService, UserManager<User> userManager)
        {
            _blogService = blogService;
            _userManager = userManager;
        }
        // GET: BrowseBlogController
        public ActionResult Index(int id)
        {
            //var user = _userManager.GetUserId(User);
            var browseBlogs = _blogService.GetBlogs();
            return View(browseBlogs);
        }

        // GET: BrowseBlogController/Details/5
        public ActionResult Views(int id)
        {
            return View();
        }
        public ActionResult Tags(int id)
        {
            return View();
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
