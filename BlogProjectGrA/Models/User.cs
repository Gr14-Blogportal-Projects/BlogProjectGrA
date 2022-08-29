using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogProjectGrA.Models
{
    public class User : IdentityUser
    {
        [Required, StringLength(32, MinimumLength = 3)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(32, MinimumLength = 3)]
        public string LastName { get; set; } = string.Empty; // = null;

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }
    }
}
