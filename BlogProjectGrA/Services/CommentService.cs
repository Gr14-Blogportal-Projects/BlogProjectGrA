using BlogProjectGrA.Data;
using BlogProjectGrA.Data.Migrations;
using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public class  CommentService : ICommentService 
    {
        private readonly ApplicationDbContext _db;

        public CommentService (ApplicationDbContext db)
        {
          _db = db;
        }
    public Comment CreateComment(Comment comment)
    {
        _db.Add(comment);
        _db.SaveChanges();
        return comment;

    }
    public void DeleteComment(Comment comment)
    {
            _db.Remove(comment);
            _db.SaveChanges();
    }

    public Comment GetComment(int id)
    {
        return _db . Comments.Find(id);
    }
    public IEnumerable<Comment> GetComments()
        {
            return _db.Comments.ToList();
        }

    public IEnumerable<Comment> GetCommentsByPost(int id)
        {
            return _db.Comments.ToList();  
        }
              
        

    public Comment UpdateComment (Comment comment)
        {
            //comment.Id = postid;
            comment.UpdatedAt = DateTime.Now;
            _db .Update(comment);
            _db .SaveChanges();
            return comment;
        }
    


    }
}
