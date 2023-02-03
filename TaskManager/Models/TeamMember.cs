using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

[Table("TeamMember")]
public partial class TeamMember
{
    [Key]
    public int IdTeamMember { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [InverseProperty("IdAssignedToNavigation")]
    public virtual ICollection<Task> TaskIdAssignedToNavigations { get; } = new List<Task>();

    [InverseProperty("IdCreatorNavigation")]
    public virtual ICollection<Task> TaskIdCreatorNavigations { get; } = new List<Task>();
}
