
using BlogProjectGrA.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace BlogProjectGrA.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Body { get; set; }

        public virtual User Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string ImageUrl { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        //public virtual ICollection<ImagesVM> Images { get; set; }

        






    }
}
