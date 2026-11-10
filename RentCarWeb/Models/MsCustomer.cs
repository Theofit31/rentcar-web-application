using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MsCustomer
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Customer_Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Password { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string Phone_Number { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string Address { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string Driver_License_Number { get; set; }

    public ICollection<TrRental> Rentals { get; set; }
}