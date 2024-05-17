using System.ComponentModel.DataAnnotations;

namespace EShop.ViewModels.UserViewModel
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
