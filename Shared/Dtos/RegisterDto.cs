using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos
{
    public class RegisterDto
    {


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }

        public string? PhoneNumber { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Display name is required")]
        public string DisplayName { get; init; }


    }
}
