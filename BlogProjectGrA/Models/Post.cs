using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        public string Body { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public int View { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        
       
        

    }
}
