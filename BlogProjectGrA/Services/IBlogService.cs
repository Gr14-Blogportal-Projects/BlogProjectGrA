using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface IBlogService
    {
        Blog GetBlog(int id);

        IEnumerable<Blog> GetBlogsByUser( string id);

        void CreateBlog(Blog blog);

        void UpdateBlog(Blog blog);

        void DeleteBlog(Blog blog);

        IEnumerable<Blog> GetBlogs();
    }
}
