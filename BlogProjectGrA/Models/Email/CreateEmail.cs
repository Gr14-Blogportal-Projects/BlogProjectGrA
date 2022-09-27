using System.Text.Json.Serialization;
namespace BlogProjectGrA.Models.Email
{
    public class CreateEmail
    {

        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("blogTitle")]
        public string BlogTitle { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

    }
}
