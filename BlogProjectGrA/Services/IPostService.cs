using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts();
        IEnumerable<Post> GetPostsByBlog(int id);  
        
        Post CreatePost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(int id);
        Post GetPost(int id);
        Post IncrementViews(int id);

        IEnumerable<Post> GetPostsByViews();
        IEnumerable<Post> GetPostsByTag(Tag tag);
        IEnumerable<Post> GetPostByDate(int blogId, int year, int month);

        string GetTagsString(IEnumerable<Tag> tags);

        Post RemovePostTags(Post post);
        void CreateImages(List<PostImage> databaseFiles);
    }
}
