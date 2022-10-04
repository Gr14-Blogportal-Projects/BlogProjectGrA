using BlogProjectGrA.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Xml.Linq;

namespace BlogProjectGrA.Models.ViewModels
{
    public class ImagesVM
    {
        public int Id { get; set; }



        public string FileName { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string ImageUrl { get; set; }

        //public virtual Post Post { get; set; }
        // public Blog Blog { get; set; }
        //public virtual ICollection<Blog> Blogs { get; set; }

        //public virtual ICollection<Post> Posts { get; set; }
    }
}
