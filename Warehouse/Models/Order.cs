using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Attributes;

namespace Warehouse.Models;

[Table("Order")]
public class Order
{
    [Key]
    public int IdOrder { get; set; }
    [Required, ForeignKey("IdProduct")]
    public int IdProduct { get; set; }
    [Required]
    public int Amount { get; set; }

    [Column(TypeName = "datetime"), ValidDateTime]
    public DateTime CreatedAt { get; set; }

    [Required, Column(TypeName = "datetime")]
    public DateTime? FulfilledAt { get; set; }
}
