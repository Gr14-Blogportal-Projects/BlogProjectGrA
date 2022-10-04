using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTriggers.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime? UpdateAt { get; set; } = null;

        public virtual Blog Blog { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public int View { get; set; }
    }
}
