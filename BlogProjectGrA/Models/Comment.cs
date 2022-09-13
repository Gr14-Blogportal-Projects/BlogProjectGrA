using System.ComponentModel.DataAnnotations;

namespace BlogProjectGrA.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        [Display(Name = "Description")]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int PostsId { get; set; }

        public virtual Post Posts { get; set; }

        public DateTime? UpdatedAt { get; set; } = null;




    }
}
