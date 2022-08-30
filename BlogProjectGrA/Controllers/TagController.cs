using BlogProjectGrA.Models;
using BlogProjectGrA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectGrA.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;

        }
        [AllowAnonymous]
        // GET: TagController
        public ActionResult Index(int id)
        {
            var tags=_tagService.GetTagsByPost(id);
            return View(tags);
        }
        [AllowAnonymous]
        // GET: TagController/Details/5
        public ActionResult Details(int id)
        {
            var tag =_tagService.GetTag(id); 
            return View(tag);
        }
        
        // GET: TagController/Create
        public ActionResult Create()
        {
            var tag = new Tag();
            return View(tag);
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            _tagService.CreatTag(tag.Name);
            return RedirectToAction(nameof(Index));
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(int id)
        {
        var tag = _tagService.GetTag(id);
            return View(tag);
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            _tagService.UpdateTag(id, tag.Name);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
           var tag= _tagService.GetTag(id);
            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            _tagService.DeleteTag(tag);
            return RedirectToAction(nameof(Index));
            
              
            
        }
    }
}
