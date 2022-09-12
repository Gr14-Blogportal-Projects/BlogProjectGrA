using System.ComponentModel.DataAnnotations;

namespace BlogProjectGrA.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string NormalizedName { get; set; }
       
        public virtual ICollection<Post> Posts { get; set; }
    }
}
