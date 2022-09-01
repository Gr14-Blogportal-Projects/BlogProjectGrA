
namespace BlogProjectGrA.Models.ViewModels
{
    public class DatesCountVM
    {
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }

    public class DateByPostVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;       
        public Blog Blog { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<DatesCountVM> Dates { get; set; }
    }
}
