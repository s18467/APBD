using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

[Table("Project")]
public partial class Project
{
    [Key]
    public int IdProject { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime Deadline { get; set; }

    [InverseProperty("IdProjectNavigation")]
    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
