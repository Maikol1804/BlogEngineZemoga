using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Models
{
    public class AuthenticationViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
