using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
        }
        [Required]
        [StringLength(40)]
        public string Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public Boolean? Status { get; set; }  


        [JsonIgnore]
        public virtual ICollection<Comment>? Comments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
