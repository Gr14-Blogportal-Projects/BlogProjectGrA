using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetComments();

        IEnumerable<Comment> GetCommentsByPost (int id);

        Comment CreateComment(Comment comment);

        void UpdateComment(Comment comment);

        void DeleteComment(int id);
    }
}
