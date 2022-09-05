using System.ComponentModel.DataAnnotations;

namespace BlogProjectGrA.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Body { get; set; }

        public virtual User Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Post> Posts { get; set; }


    }
}
