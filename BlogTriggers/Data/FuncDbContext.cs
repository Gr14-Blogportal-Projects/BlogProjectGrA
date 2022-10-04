using BlogTriggers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogTriggers.Data
{
    public class FuncDbContext: DbContext
    {
        public FuncDbContext(DbContextOptions<FuncDbContext> options):base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> AspNetUsers { get; set; }
    }
}
