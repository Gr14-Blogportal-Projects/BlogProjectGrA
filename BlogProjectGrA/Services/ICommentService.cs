using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface ICommentService
    {
        Comment GetComment(int id); 
        IEnumerable<Comment> GetComments();

        IEnumerable<Comment> GetCommentsByPost (int id);

        Comment CreateComment(Comment comment);

        Comment UpdateComment(Comment comment);

        void DeleteComment(int id);
    }
}
