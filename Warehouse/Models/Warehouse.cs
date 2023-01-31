using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Models;

[Table("Warehouse")]
public class Warehouse
{
    [Key]
    public int IdWarehouse { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = null!;

    [Required, StringLength(200)]
    public string Address { get; set; } = null!;
}
