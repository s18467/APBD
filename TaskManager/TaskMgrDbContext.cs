using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager;

public partial class TaskMgrDbContext : DbContext
{
    public TaskMgrDbContext()
    {
    }

    public TaskMgrDbContext(DbContextOptions<TaskMgrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("Project_pk");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("Task_pk");

            entity.HasOne(d => d.IdAssignedToNavigation).WithMany(p => p.TaskIdAssignedToNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TeamMember2");

            entity.HasOne(d => d.IdCreatorNavigation).WithMany(p => p.TaskIdCreatorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TeamMember1");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_Project");

            entity.HasOne(d => d.IdTaskTypeNavigation).WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Task_TaskType");
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.HasKey(e => e.IdTaskType).HasName("TaskType_pk");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.IdTeamMember).HasName("TeamMember_pk");
        });

    }

}
