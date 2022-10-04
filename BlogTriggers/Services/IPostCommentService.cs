using BlogTriggers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTriggers.Services
{

    public interface IPostCommentService
    {
        List<PostComment> GetPostsWithRecentComments();
    }
}
