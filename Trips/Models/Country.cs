using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trips.Models;

[Table("Country")]
public partial class Country
{
    [Key]
    public int IdCountry { get; set; }

    [StringLength(120)]
    public string Name { get; set; } = null!;

    [ForeignKey("IdCountry")]
    [InverseProperty("IdCountries")]
    public virtual ICollection<Trip> IdTrips { get; } = new List<Trip>();
}
