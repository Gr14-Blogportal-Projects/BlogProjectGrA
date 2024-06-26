﻿using BlogProjectGrA.Models;

namespace BlogProjectGrA.Models.ViewModels
{
    public class PostsTagsViewModel
    {
        public Tag SelectedTag { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Post> Posts { get; set; }

    }
}
