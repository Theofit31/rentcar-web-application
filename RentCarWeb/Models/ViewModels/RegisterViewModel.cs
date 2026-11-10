using System.ComponentModel.DataAnnotations;

namespace RentCarWeb.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nama lengkap wajib diisi")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nama minimal 3 karakter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Konfirmasi password wajib diisi")]
        [Compare("Password", ErrorMessage = "Password tidak cocok")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Nomor telepon hanya boleh angka (10–15 digit)")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Alamat wajib diisi")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Alamat minimal 5 karakter")]
        public string Address { get; set; }
    }
}
