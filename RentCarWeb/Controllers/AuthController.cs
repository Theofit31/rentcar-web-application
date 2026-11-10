using Microsoft.AspNetCore.Mvc;
using RentCarWeb.Data;
using RentCarWeb.Models;
using RentCarWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;


namespace RentCarWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly RentCarDbContext _context;

        public AuthController(RentCarDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Cek email sudah digunakan
            var exists = _context.MsCustomers
                .FirstOrDefault(c => c.Email.ToLower() == model.Email.ToLower());

            if (exists != null)
            {
                ModelState.AddModelError("Email", "Email sudah terdaftar, gunakan email lain");
                return View(model);
            }

            // Jika lolos semua validasi
            var hasher = new PasswordHasher<MsCustomer>();
            var hashedPassword = hasher.HashPassword(null, model.Password);

            var customer = new MsCustomer
            {
                Name = model.Name.Trim(),
                Email = model.Email.Trim(),
                Password = hashedPassword,
                Phone_Number = model.Phone.Trim(),
                Address = model.Address.Trim(),
                Driver_License_Number = ""
            };

            _context.MsCustomers.Add(customer);
            _context.SaveChanges();

            TempData["Success"] = "Registrasi berhasil! Silakan login.";

            return RedirectToAction("Login", "Auth");
        }

        // GET: Login
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // POST: Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.MsCustomers.FirstOrDefault(c => c.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email tidak ditemukan");
                return View(model);
            }

            var hasher = new PasswordHasher<MsCustomer>();
            var result = hasher.VerifyHashedPassword(null, user.Password, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Password salah");
                return View(model);
            }

            // Simpan session
            HttpContext.Session.SetString("UserId", user.Customer_Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserEmail", user.Email);

            // Redirect ke halaman sebelum login
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(string? returnUrl)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
                return RedirectToAction("Login");

            var user = _context.MsCustomers.FirstOrDefault(c => c.Customer_Id == userId);

            if (user == null)
                return NotFound();

            ViewData["ReturnUrl"] = returnUrl;

            return View(user);
        }

        [HttpPost]
        public IActionResult Profile(MsCustomer model, string? returnUrl)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
                return RedirectToAction("Login");

            var user = _context.MsCustomers.FirstOrDefault(c => c.Customer_Id == userId);
            if (user == null)
                return NotFound();

            void RestoreReturnUrl()
            {
                ViewBag.ReturnUrl = returnUrl;
                model.Email = user.Email;
            }

            // VALIDASI NAMA
            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 3)
            {
                TempData["Error"] = "Nama minimal 3 karakter.";
                RestoreReturnUrl();
                model.Email = user.Email;
                return View(model);
            }

            // VALIDASI NOMOR TELEPON
            if (!System.Text.RegularExpressions.Regex.IsMatch(model.Phone_Number ?? "", @"^\d{10,15}$"))
            {
                TempData["Error"] = "Nomor telepon harus berupa angka (10–15 digit).";
                RestoreReturnUrl();
                model.Email = user.Email;
                return View(model);
            }

            // VALIDASI ALAMAT
            if (string.IsNullOrWhiteSpace(model.Address) || model.Address.Length < 5)
            {
                TempData["Error"] = "Alamat tidak boleh kosong.";
                RestoreReturnUrl();
                model.Email = user.Email;
                return View(model);
            }

            // VALIDASI NOMOR SIM
            if (!string.IsNullOrEmpty(model.Driver_License_Number) &&
                !System.Text.RegularExpressions.Regex.IsMatch(model.Driver_License_Number, @"^\d{14,16}$"))
            {
                TempData["Error"] = "Nomor SIM tidak valid. Harus angka 14–16 digit.";
                RestoreReturnUrl();
                model.Email = user.Email;
                return View(model);
            }

            // UPDATE DATA
            user.Name = model.Name;
            user.Phone_Number = model.Phone_Number;
            user.Address = model.Address;
            user.Driver_License_Number = model.Driver_License_Number;

            _context.SaveChanges();

            TempData["Success"] = "Profil berhasil diperbarui!";

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Profile");
        }
    }
}
