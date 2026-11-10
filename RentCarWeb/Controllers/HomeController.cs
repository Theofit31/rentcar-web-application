using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarWeb.Data;

public class HomeController : Controller
{
    private readonly RentCarDbContext _context;

    public HomeController(RentCarDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(DateTime? pickupDate, DateTime? returnDate, int? yearFilter, string? search, int page = 1)
    {
        int pageSize = 3;

        ViewBag.PickupDate = pickupDate?.ToString("yyyy-MM-dd");
        ViewBag.ReturnDate = returnDate?.ToString("yyyy-MM-dd");
        ViewBag.YearFilter = yearFilter;
        ViewBag.Search = search;
        ViewBag.Page = page;

        TempData["PickupDate"] = pickupDate;
        TempData["ReturnDate"] = returnDate;
        TempData["YearFilter"] = yearFilter;

        // kalau belum isi tanggal, jangan tampilkan mobil
        if (pickupDate == null || returnDate == null)
        {
            return View();
        }

        // ambil semua mobil aktif
        var cars = _context.MsCars
            .Include(c => c.CarImages)
            .Where(c => c.Status == true)
            .AsQueryable();


        // FILTER SEARCH
        if (!string.IsNullOrWhiteSpace(search))
        {
            string keyword = search.ToLower();
            cars = cars.Where(c =>
                c.Name.ToLower().Contains(keyword) ||
                c.Model.ToLower().Contains(keyword) ||
                c.Transmission.ToLower().Contains(keyword) ||
                c.Year.ToString().Contains(keyword)
            );
        }

        // filter tahun pembuatan
        if (yearFilter.HasValue)
        {
            cars = cars.Where(c => c.Year == yearFilter.Value);
        }

        // ambil semua id mobil yang sedang disewa dalam rentang tanggal
        var rentedCarIds = _context.TrRentals
            .Where(r => r.Rental_Date <= returnDate &&
                        r.Return_Date >= pickupDate)
            .Select(r => r.Car_Id)
            .ToList();
        ;

        // hanya tampilkan mobil yang tidak disewa
        cars = cars.Where(c => !rentedCarIds.Contains(c.Car_Id));

        int totalCars = cars.Count();
        int totalPages = (int)Math.Ceiling(totalCars / (double)pageSize);

        ViewBag.TotalPages = totalPages;

        var paginatedCars = cars
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return View(paginatedCars);
    }
    public IActionResult Contact()
    {
        return View();
    }
}