using System.ComponentModel.DataAnnotations;

namespace BlogProjectGrA.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User Author { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
