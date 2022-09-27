﻿using System.Web.Mvc;

namespace BlogProjectGrA.Models.ViewModels
{
    public class CreatePostVM
    {
        public int Id { get; set; }

        
        
        public string Title { get; set; }


        public string Body { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public int View { get; set; }
        public int BlogId { get; set; }
        //public string FileName { get; set; }

        //public IFormFile File { get; set; }
        //public virtual ICollection<Post>Posts { get; set; }

        public IList<SelectListItem>Blogs { get; set; }
    }
}
