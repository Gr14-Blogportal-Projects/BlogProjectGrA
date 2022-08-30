using BlogProjectGrA.Data;
using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _db;
        public TagService(ApplicationDbContext db)
        {
            _db= db;
        }
        public Tag CreatTag(string name)
        {

            var tag = new Tag
            {
                Name = name,
                NormalizedName = name.ToUpper()

            };
            _db.Add(tag);
            _db.SaveChanges();
            return tag;
        }

        public void DeleteTag(Tag tag)
        {
            _db.Remove(tag);
            _db.SaveChanges();
        }

        public Tag GetTag(int id)
        {
            return _db.Tags.Find(id);
        }

        public IEnumerable<Tag> GetTags()
        {
            return _db.Tags.ToList();
        }

        public Tag UpdateTag(int id, string name)
        {
            var tag = GetTag(id);
            tag.Name = name;
            tag.NormalizedName=name.ToUpper();
            _db.Update(tag);
            _db.SaveChanges();

            return tag;
        }

        public Tag UpdateTag(Tag tag)
        {
            return UpdateTag(tag.Id, tag.Name);

        }
        public IEnumerable<Tag> GetTagsByPost(int id)
        {
            var tags = _db.Tags.Where(t => t.Posts.FirstOrDefault(p => p.Id == id) != null).ToList();

            return tags;
            //return _db.Tags.Find(id);



        }
        //public Tag IncrementViews(int id)
        //{
        //    var tag = GetTag(id);
        //    tag.View++;
        //    _db.Update(tag);
        //    _db.SaveChanges();
        //    return tag;

    }
}
