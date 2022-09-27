using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using BlogProjectGrA.Models.ViewModels;

namespace BlogProjectGrA.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        [Display(Name = "Description")]
        public string Body { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime? UpdateAt { get; set; } = null;
        public int View { get; set; }

        public virtual Blog Blog { get; set; }
       // public string ImageUrl { get; set; }


       // public virtual CreatePostVM CreatePostVM { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }

        //public virtual ICollection<ImagesVM> Images { get; set; }
        public string GetTagsString()
        {
            var tagNames = Tags.Select(p => p.Name).ToList();
            var tagsString = string.Join(",", tagNames);
            return tagsString;
        }

        public int GetViews()
        {
            return View;
        }

    }
}
