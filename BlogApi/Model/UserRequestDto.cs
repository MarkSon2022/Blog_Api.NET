using System.ComponentModel.DataAnnotations;

namespace BlogApi.Model
{
    public class UserDto
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "Username can not be empty")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can not be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role can not be empty")]
        public string Role { get; set; }
    }
}
