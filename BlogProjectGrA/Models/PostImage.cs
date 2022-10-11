using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProjectGrA.Models
{
    public class PostImage
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public virtual Post Post { get; set; }

    }
}
