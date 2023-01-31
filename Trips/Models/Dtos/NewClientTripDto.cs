using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models.Dtos
{
    public class NewClientTripDto
    {
        [Required, StringLength(120)]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(120)]
        public string LastName { get; set; } = null!;

        [Required, StringLength(120)]
        public string Email { get; set; } = null!;

        [Required, StringLength(120)]
        public string Telephone { get; set; } = null!;

        [Required, StringLength(120)]
        public string Pesel { get; set; } = null!;
        [Required]
        public int IdTrip { get; set; }
        [StringLength(120)]
        public string TripName { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
    }
}
