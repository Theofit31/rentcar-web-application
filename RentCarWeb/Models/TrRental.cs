using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TrRental
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Rental_Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime Rental_Date { get; set; }
    public DateTime Return_Date { get; set; }

    [Precision(18, 2)]
    public decimal Total_Price { get; set; }

    public bool Payment_Status { get; set; }

    [ForeignKey("Customer")]
    [Column(TypeName = "nvarchar(36)")]
    public string Customer_Id { get; set; }
    public MsCustomer Customer { get; set; }

    [ForeignKey("Car")]
    [Column(TypeName = "nvarchar(36)")]
    public string Car_Id { get; set; }
    public MsCar Car { get; set; }

    public ICollection<LtPayment> Payments { get; set; }
}
