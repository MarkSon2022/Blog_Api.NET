using System.ComponentModel.DataAnnotations;

namespace BlogApi.Model
{
    public class CommentDto
    {
        [Required]
        public int Id { get; set; }
        public string Content { get; set; }
        [Required]
        public int? PostId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
