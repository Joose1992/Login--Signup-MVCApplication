using System.ComponentModel.DataAnnotations;

namespace Login__Signup_MVCApplication.Models
{
    public class RegisterViewModel
    {
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool IsActionSuccess { get; set; }
        public string ActionMessage { get; set; }
    }
}
