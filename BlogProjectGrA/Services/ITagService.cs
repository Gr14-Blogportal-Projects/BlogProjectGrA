using BlogProjectGrA.Models;

namespace BlogProjectGrA.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> GetTags();
        Tag GetTag(int id);
        Tag CreatTag(String name);
        void DeleteTag(Tag tag);
        Tag UpdateTag(int id, string name);
        Tag UpdateTag(Tag tag);
    }
}
