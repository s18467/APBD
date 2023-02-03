using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

[Table("TaskType")]
public partial class TaskType
{
    [Key]
    public int IdTaskType { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("IdTaskTypeNavigation")]
    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
