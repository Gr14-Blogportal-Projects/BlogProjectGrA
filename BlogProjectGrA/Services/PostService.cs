﻿using BlogProjectGrA.Data;
using BlogProjectGrA.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectGrA.Services
{
   
    public class PostService : IPostService
    {

        private readonly ApplicationDbContext _db;

        public PostService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Post CreatePost(Post post)
        {
            _db.Add(post);
            _db.SaveChanges();
            return post;

        }

        public void DeletePost(int id)
        {
            var post = GetPost(id);
            _db.Remove(post);
            _db.SaveChanges();
        }
        public Post UpdatePost(Post post)
        {
            post.UpdateAt = DateTime.Now;
            _db.Update(post);
            _db.SaveChanges();
            return post;
        }
        

        public IEnumerable<Post> GetPosts()
        {
            return _db.Posts.ToList();
        }

        public IEnumerable<Post> GetPostsByBlog(int id)
        {
            return _db.Posts.ToList();

        }

        public Post GetPost(int id) 
        {
           return _db.Posts.Find(id);
        }

        public Post IncrementViews(int id)
        {
            var post = GetPost(id);
            post.View++;
            _db.Update(post);
            _db.SaveChanges();
            return post;
        }

        public IEnumerable<Post> GetPostsByViews()
        {
            var posts = _db.Posts.OrderByDescending(p => p.View).ToList();
            return posts;
        }
        public IEnumerable<Post> GetPostsByTag(Tag tag)
        {
            var posts = _db.Posts.Where(p => p.Tags.Contains(tag)).ToList();
            return posts;
        }
        public IEnumerable<Post> GetPostByDate(int blogId, int year, int month)
        {
            var posts = _db.Posts
                .Where(p => p.Blog.Id ==  blogId && p.CreateAt.Year == year && p.CreateAt.Month == month)
                .OrderByDescending(p => p.CreateAt)
                .ToList();
            
            return posts;
        }

        public string GetTagsString(IEnumerable<Tag> tags)
        {
            var tagNames = tags.Select(p => p.Name).ToList();
            var tagsString = string.Join(",", tagNames);
            return tagsString;
        }

        public Post RemovePostTags(Post post)
        {
            post.Tags.Clear();
            _db.Update(post);
            _db.SaveChanges();
            return post;
        }
    }
}
