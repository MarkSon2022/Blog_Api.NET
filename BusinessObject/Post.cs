using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string AuthorId { get; set; }
        public int? CategoryId { get; set; }

        
        public virtual User? Author { get; set; }
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment>? Comments { get; set; } = null!;
    }
}
