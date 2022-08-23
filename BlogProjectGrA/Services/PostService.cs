using BlogProjectGrA.Data;
using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
   
    public class PostService : IPostService
    {

        private readonly ApplicationDbContext _db;

        public PostService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Post CreatePost(Post post)
        {
            _db.Add(post);
            _db.SaveChanges();
            return post;

        }

        public void DeletePost(int id)
        {
            _db.Remove(id);
            _db.SaveChanges();
            return; 
        }
        public Post UpdatePost(Post post)
        {
            _db.Update(post);
            _db.SaveChanges();
            return post;
        }
        

        public IEnumerable<Post> GetPosts()
        {
            return _db.Posts.ToList();
        }

        public IEnumerable<Post> GetPostsByBlog(int Id)
        {
            return _db.Posts.ToList();

        }

        public Post IncrementViews(int id)
        {
            var post = _db.Posts.Find(id);

            return post ;
        }
    }
}
