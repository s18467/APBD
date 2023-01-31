using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

[Table("Product_Warehouse")]
public class ProductWarehouse
{
    [Key]
    public int IdProductWarehouse { get; set; }
    [Required, ForeignKey("IdWarehouse")]
    public int IdWarehouse { get; set; }
    [Required, ForeignKey("IdProduct")]
    public int IdProduct { get; set; }
    [Required, ForeignKey("IdOrder")]
    public int IdOrder { get; set; }
    [Required]
    public int Amount { get; set; }

    [Required, Column(TypeName = "numeric(25, 2)")]
    public decimal Price { get; set; }

    [Required, Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }
}
