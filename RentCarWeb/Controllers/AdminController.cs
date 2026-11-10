using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarWeb.Data;

namespace RentCarWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly RentCarDbContext _context;
        public AdminController(RentCarDbContext context)
        {
            _context = context;
        }
        // Dashboard Home
        public IActionResult Index()
        {
            ViewBag.TotalCars = _context.MsCars.Count();
            ViewBag.TotalCustomers = _context.MsCustomers.Count();
            ViewBag.TotalTransactions = _context.TrRentals.Count();
            ViewBag.PaidTransactions = _context.TrRentals.Count(r => r.Payment_Status == true);
            ViewBag.UnpaidTransactions = _context.TrRentals.Count(r => r.Payment_Status == false);

            return View();
        }

        // List Mobil
        public IActionResult Cars()
        {
            var cars = _context.MsCars
                .Include(c => c.CarImages)
                .ToList();

            return View(cars);
        }

        // List Customer
        public IActionResult Customers()
        {
            var customers = _context.MsCustomers.ToList();
            return View(customers);
        }

        // List Transaksi
        public IActionResult Transactions()
        {
            var rentals = _context.TrRentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .ToList();

            return View(rentals);
        }
    }
}
