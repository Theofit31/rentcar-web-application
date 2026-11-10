using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TrMaintenance
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Maintenance_Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime Maintenance_Date { get; set; }

    [Column(TypeName = "nvarchar(4000)")]
    public string Decription { get; set; }

    [Precision(18, 2)]
    public decimal Cost { get; set; }

    [ForeignKey("Car")]
    [Column(TypeName = "nvarchar(36)")]
    public string Car_Id { get; set; }
    public MsCar Car { get; set; }

    [ForeignKey("Employee")]
    [Column(TypeName = "nvarchar(36)")]
    public string Employee_Id { get; set; }
    public MsEmployee Employee { get; set; }
}
