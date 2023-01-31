using System.ComponentModel.DataAnnotations;
using Warehouse.Attributes;

namespace Warehouse.ViewModels
{
    public class OrderPostViewModel
    {
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdWarehouse { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Amount { get; set; }
        [Required, ValidDateTime]
        public DateTime CreatedAt { get; set; }
    }
}
