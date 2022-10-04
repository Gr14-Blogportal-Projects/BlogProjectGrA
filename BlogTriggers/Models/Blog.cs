using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTriggers.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public virtual User Author { get; set; }
    }
}
