using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface IBlogService
    {
        Blog GetBlog(int id);

        void CreateBlog(Blog blog);

        void UpdateBlog(Blog blog);

        void DeleteBlog(int id);

        IEnumerable<Blog> GetBlogs();
    }
}
