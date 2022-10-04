using BlogTriggers.Data;
using BlogTriggers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTriggers.Services
{

    public class PostCommentService:IPostCommentService
    {
        private readonly FuncDbContext _db;

        public PostCommentService( FuncDbContext db)
        {
            _db = db;
        }

        public List<PostComment> GetPostsWithRecentComments()
        {
            var last24 = DateTime.Now.AddDays(-1);
            var postComments = _db.Posts
                .Include(c => c.Comments)
                .Include(c => c.Blog)
                .Include(c => c.Blog.Author)
                .Where(p => p.Comments.Any(c => c.CreatedAt > last24))
                .Select(p => new PostComment { User = p.Blog.Author, CommentCount = p.Comments.Where(c => c.CreatedAt > last24).Count(), PostTitle = p.Title })
                .ToList();
            return postComments;
        }
    }
}
