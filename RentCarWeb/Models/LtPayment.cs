using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LtPayment
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Payment_Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime Payment_Date { get; set; }

    [Precision(18, 2)]
    public decimal Amount { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Payment_Method { get; set; }

    [ForeignKey("Rental")]
    [Column(TypeName = "nvarchar(36)")]
    public string Rental_Id { get; set; }

    public TrRental Rental { get; set; }
}
