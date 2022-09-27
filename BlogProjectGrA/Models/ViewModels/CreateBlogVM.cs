using BlogProjectGrA.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Xml.Linq;

namespace BlogProjectGrA.Models.ViewModels
{
    public class CreateBlogVM
    {
        public string FileName { get; set; }

        public IFormFile File { get; set; }

        public Blog Blog { get; set; }
    }
}
