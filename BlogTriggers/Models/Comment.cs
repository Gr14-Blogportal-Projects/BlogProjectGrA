using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTriggers.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string AuthorId { get; set; }

        public int PostsId { get; set; }

        public virtual Post Posts { get; set; }

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
