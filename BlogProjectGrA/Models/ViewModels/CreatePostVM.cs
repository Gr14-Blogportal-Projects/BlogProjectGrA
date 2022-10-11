using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProjectGrA.Models.ViewModels
{
    public class CreatePostVM
    {
        public IFormFileCollection Files { get; set; }

        public Post Post { get; set; }
    }
}
