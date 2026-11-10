using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MsCarImages
{
    [Key]
    [Column(TypeName = "nvarchar(36)")]
    public string Image_Car_Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey("Car")]
    [Column(TypeName = "nvarchar(36)")]
    public string Car_Id { get; set; }

    [Column(TypeName = "nvarchar(2000)")]
    public string Image_Link { get; set; }

    public MsCar Car { get; set; }
}
