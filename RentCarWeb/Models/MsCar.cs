using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MsCar
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Car_Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Model { get; set; }

    public int Year { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string License_Plate { get; set; }

    public int Number_Of_Car_Seats { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Transmission { get; set; }

    [Precision(18, 2)]
    public decimal Price_Per_Day { get; set; }

    public bool Status { get; set; }

    public ICollection<MsCarImages> CarImages { get; set; }
    public ICollection<TrRental> Rentals { get; set; }
    public ICollection<TrMaintenance> Maintenances { get; set; }
}
