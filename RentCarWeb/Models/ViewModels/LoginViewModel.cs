using System.ComponentModel.DataAnnotations;

namespace RentCarWeb.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        public string Password { get; set; }
    }
}
