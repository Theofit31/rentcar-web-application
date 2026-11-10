using Microsoft.EntityFrameworkCore;
using RentCarWeb.Models;

namespace RentCarWeb.Data
{
    public class RentCarDbContext : DbContext
    {
        public RentCarDbContext(DbContextOptions<RentCarDbContext> options)
            : base(options)
        {
        }

        public DbSet<MsCustomer> MsCustomers { get; set; }
        public DbSet<MsCar> MsCars { get; set; }
        public DbSet<MsCarImages> MsCarImages { get; set; }
        public DbSet<TrRental> TrRentals { get; set; }
        public DbSet<LtPayment> LtPayments { get; set; }
        public DbSet<TrMaintenance> TrMaintenances { get; set; }
        public DbSet<MsEmployee> MsEmployees { get; set; }
    }
}