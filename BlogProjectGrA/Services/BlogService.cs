using BlogProjectGrA.Data;
using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _db;

        public BlogService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void CreateBlog(Blog blog)
        {
            _db.Add(blog);
            _db.SaveChanges();
        }

        public void DeleteBlog(int id)
        {
            var blog = GetBlog(id);
            _db.Remove(blog);
            _db.RemoveRange(blog.Posts);
            _db.SaveChanges();
            DeleteImageFile(blog);
        }
        
        public void DeleteImageFile(Blog blog)
        {
            try
            {
                var deleteURL = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.ImageUrl);
                File.Delete(deleteURL);
            }
            catch
            {

            }
        }
        public Blog GetBlog(int id)
        {
            var blog = _db.Blogs.Find(id);
            return blog;
        }

        public IEnumerable<Blog> GetBlogs()
        {
            return _db.Blogs.ToList();

        }
        public IEnumerable<Blog> GetBlogsByUser(string id)
        {
            return _db.Blogs.Where(b =>b.Author.Id == id).ToList();   
            
        }

        public void UpdateBlog(Blog blog)
        {
            //_db.Blogs.Find(id);
            _db.Update(blog);
            _db.SaveChanges();

        }
        
    }
}
