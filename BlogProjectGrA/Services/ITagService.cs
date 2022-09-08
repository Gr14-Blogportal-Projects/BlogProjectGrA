using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> GetTags();
        public IEnumerable<Tag> GetTagsByPost(int id);
        Tag GetTag(int id);
        Tag CreatTag(String name);
        void DeleteTag(Tag tag);
        Tag UpdateTag(int id, string name);
        Tag UpdateTag(Tag tag);

        IEnumerable<Tag> GetOrCreateTags(string[] tagList);
    }
}
