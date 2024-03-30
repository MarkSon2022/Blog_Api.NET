using BusinessObject;

namespace BlogApi.Model
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
