using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

[Table("Product")]
public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = null!;

    [Required, StringLength(200)]
    public string Description { get; set; } = null!;

    [Required, Column(TypeName = "numeric(25, 2)")]
    public decimal Price { get; set; }
}
