using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.Dtos.User
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
