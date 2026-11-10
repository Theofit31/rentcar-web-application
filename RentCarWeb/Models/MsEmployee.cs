using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MsEmployee
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Employee_Id { get; set; } = Guid.NewGuid().ToString();

    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }

    [Column(TypeName = "nvarchar(4000)")]
    public string Position { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string Email { get; set; }

    [Column(TypeName = "nvarchar(36)")]
    public string Phone_Number { get; set; }

    public ICollection<TrMaintenance> Maintenances { get; set; }
}
