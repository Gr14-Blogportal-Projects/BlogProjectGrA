using BlogProjectGrA.Models;

namespace BlogProjectGrA.ViewModels
{
    public class SearchViewModel
    {

        public IEnumerable<Blog> Blogs { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
