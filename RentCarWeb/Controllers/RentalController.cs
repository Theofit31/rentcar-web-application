using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarWeb.Data;
using RentCarWeb.Models;

public class RentalController : Controller
{
    private readonly RentCarDbContext _context;

    public RentalController(RentCarDbContext context)
    {
        _context = context;
    }

    // GET: /Rental/Detail/{id}?pickupDate=...&returnDate=...
    public IActionResult Detail(string id, DateTime pickupDate, DateTime returnDate)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        // Ambil data user
        var user = _context.MsCustomers.FirstOrDefault(c => c.Customer_Id == userId);

        // Jika SIM belum diisi → redirect ke halaman profil
        if (string.IsNullOrEmpty(user.Driver_License_Number))
        {
            TempData["NeedLicense"] = "Lengkapi nomor SIM Anda sebelum menyewa mobil.";
            // Buat URL balik ke halaman Detail mobil
            var returnUrl = Url.Action("Detail", "Rental", new { id, pickupDate, returnDate });

            // Redirect ke halaman profil dengan returnUrl
            return RedirectToAction("Profile", "Auth", new { returnUrl });
        }

        var car = _context.MsCars
            .Include(c => c.CarImages)
            .FirstOrDefault(c => c.Car_Id == id);

        if (car == null)
            return NotFound();

        var days = (returnDate - pickupDate).Days + 1;
        if (days <= 0) days = 1;

        ViewBag.Car = car;
        ViewBag.PickupDate = pickupDate.ToString("dd MMM yyyy");
        ViewBag.ReturnDate = returnDate.ToString("dd MMM yyyy");
        ViewBag.Days = days;
        ViewBag.TotalPrice = days * car.Price_Per_Day;

        ViewBag.UserName = user.Name;
        ViewBag.UserEmail = user.Email;
        return View();
    }


    // POST: /Rental/Confirm
    [HttpPost]
    public IActionResult Confirm(string carId, DateTime pickupDate, DateTime returnDate, decimal totalPrice)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var rental = new TrRental
        {
            Rental_Id = Guid.NewGuid().ToString(),
            Rental_Date = pickupDate,
            Return_Date = returnDate,
            Total_Price = totalPrice,
            Payment_Status = false,
            Customer_Id = userId,
            Car_Id = carId
        };

        _context.TrRentals.Add(rental);
        _context.SaveChanges();

        TempData["Success"] = "Penyewaan berhasil! Silakan lakukan pembayaran";
        return RedirectToAction("Index", "Home");
    }

    public IActionResult MyRentals()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        // Ambil semua rental milik user
        var rentals = _context.TrRentals
            .Include(r => r.Car)
            .Where(r => r.Customer_Id == userId)
            .OrderByDescending(r => r.Rental_Date)
            .ToList();

        return View(rentals);
    }

    // Halaman pembayaran
    [HttpGet]
    public IActionResult Payment(string rentalId)
    {
        var rental = _context.TrRentals
            .Include(r => r.Car)
            .FirstOrDefault(r => r.Rental_Id == rentalId);

        if (rental == null)
            return NotFound();

        var totalDays = (rental.Return_Date - rental.Rental_Date).Days + 1;

        ViewBag.RentalId = rental.Rental_Id;
        ViewBag.CarName = $"{rental.Car?.Name} {rental.Car?.Model}";
        ViewBag.PricePerDay = rental.Car?.Price_Per_Day;
        ViewBag.TotalDays = totalDays;
        ViewBag.TotalPrice = rental.Total_Price;

        return View();
    }

    // Konfirmasi pembayaran
    [HttpPost]
    public IActionResult ConfirmPayment(string rentalId, string paymentMethod)
    {
        var rental = _context.TrRentals.FirstOrDefault(r => r.Rental_Id == rentalId);
        if (rental == null)
            return NotFound();

        // Buat record baru di LtPayment
        var payment = new LtPayment
        {
            Payment_Id = Guid.NewGuid().ToString(),
            Rental_Id = rentalId,
            Payment_Method = paymentMethod,
            Amount = rental.Total_Price,
            Payment_Date = DateTime.Now
        };

        _context.LtPayments.Add(payment);

        // Update status rental
        rental.Payment_Status = true;

        _context.SaveChanges();

        TempData["Success"] = "Pembayaran berhasil dikonfirmasi!";
        return RedirectToAction("MyRentals");
    }
    // Batalkan pesanan
    [HttpPost]
    public IActionResult Cancel(string rentalId)
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (userId == null)
            return RedirectToAction("Login", "Auth");

        var rental = _context.TrRentals
            .FirstOrDefault(r => r.Rental_Id == rentalId && r.Customer_Id == userId);

        if (rental == null)
            return NotFound();

        // Hanya bisa dibatalkan jika BELUM dibayar
        if (rental.Payment_Status)
        {
            TempData["Error"] = "Transaksi sudah dibayar dan tidak bisa dibatalkan.";
            return RedirectToAction("MyRentals");
        }

        // Hapus rental
        _context.TrRentals.Remove(rental);
        _context.SaveChanges();

        TempData["Success"] = "Pesanan berhasil dibatalkan.";

        return RedirectToAction("MyRentals");
    }
}