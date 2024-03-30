using System.ComponentModel.DataAnnotations;

namespace BlogApi.Model
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
