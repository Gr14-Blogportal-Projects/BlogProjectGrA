using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts();
        IEnumerable<Post> GetPostsByBlog(int Id);  
        
        Post CreatePost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(int id);
        Post GetPost(int id);
        Post IncrementViews(int id);

    }
}
