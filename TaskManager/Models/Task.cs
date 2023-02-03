using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

[Table("Task")]
public partial class Task
{
    [Key]
    public int IdTask { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime Deadline { get; set; }

    public int IdProject { get; set; }

    public int IdTaskType { get; set; }

    public int IdAssignedTo { get; set; }

    public int IdCreator { get; set; }

    [ForeignKey("IdAssignedTo")]
    [InverseProperty("TaskIdAssignedToNavigations")]
    public virtual TeamMember IdAssignedToNavigation { get; set; } = null!;

    [ForeignKey("IdCreator")]
    [InverseProperty("TaskIdCreatorNavigations")]
    public virtual TeamMember IdCreatorNavigation { get; set; } = null!;

    [ForeignKey("IdProject")]
    [InverseProperty("Tasks")]
    public virtual Project IdProjectNavigation { get; set; } = null!;

    [ForeignKey("IdTaskType")]
    [InverseProperty("Tasks")]
    public virtual TaskType IdTaskTypeNavigation { get; set; } = null!;
}
