using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager;

public class TaskMgrDbContext : DbContext
{
    public TaskMgrDbContext(DbContextOptions<TaskMgrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; } = null!;
    public virtual DbSet<Task> Tasks { get; set; } = null!;
    public virtual DbSet<TaskType> TaskTypes { get; set; } = null!;
    public virtual DbSet<TeamMember> TeamMembers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddKeys(modelBuilder);
        //Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>().HasData(
            new Project { IdProject = 1, Name = "Aplikacja webowa Gakko", Deadline = new DateTime(2021, 02, 03) },
            new Project { IdProject = 2, Name = "Aplikacja mobilna KES", Deadline = new DateTime(2018, 02, 04) },
            new Project { IdProject = 3, Name = "Aplikacja webowa ZeroGame", Deadline = new DateTime(2020, 04, 05) }
        );

        modelBuilder.Entity<Task>().HasData(
            new Task
            {
                IdTask = 1,
                Name = "Przygotowanie repozytorium",
                Description = "Umieszczenie pliku .gitignore",
                Deadline = new DateTime(2020, 06, 12),
                IdProject = 1,
                IdTaskType = 1,
                IdAssignedTo = 1,
                IdCreator = 2
            },
            new Task
            {
                IdTask = 2,
                Name = "Przygotowanie pierwszej strony",
                Description = "Pierwsza strona powinna zawierać okno wyszukiwania",
                Deadline = new DateTime(2020, 06, 14),
                IdProject = 2,
                IdTaskType = 2,
                IdAssignedTo = 1,
                IdCreator = 3
            },
            new Task
            {
                IdTask = 3,
                Name = "Implement facebook logging",
                Description = "Use FB API",
                Deadline = new DateTime(2020, 05, 02),
                IdProject = 1,
                IdTaskType = 1,
                IdAssignedTo = 2,
                IdCreator = 1
            },
            new Task
            {
                IdTask = 4,
                Name = "Implement details page",
                Description = "Details in doc",
                Deadline = new DateTime(2020, 05, 04),
                IdProject = 1,
                IdTaskType = 2,
                IdAssignedTo = 1,
                IdCreator = 1
            },
            new Task
            {
                IdTask = 5,
                Name = "Implement contact apge",
                Description = "Details in the doc",
                Deadline = new DateTime(2020, 07, 02),
                IdProject = 2,
                IdTaskType = 2,
                IdAssignedTo = 1,
                IdCreator = 2
            },
            new Task
            {
                IdTask = 8,
                Name = "User validation mechanic",
                Description = "Details in the doc",
                Deadline = new DateTime(2020, 05, 04),
                IdProject = 3,
                IdTaskType = 1,
                IdAssignedTo = 2,
                IdCreator = 1
            },
            new Task
            {
                IdTask = 10,
                Name = "Add update user endpoint",
                Description = "Create API endpoint",
                Deadline = new DateTime(2020, 05, 10),
                IdProject = 3,
                IdTaskType = 2,
                IdAssignedTo = 1,
                IdCreator = 1
            }
        );

        modelBuilder.Entity<TaskType>().HasData(
            new TaskType { IdTaskType = 1, Name = "Backend" },
            new TaskType { IdTaskType = 2, Name = "Frontend" }
        );

        modelBuilder.Entity<TeamMember>().HasData(
            new TeamMember
            {
                IdTeamMember = 1,
                FirstName = "Jan",
                LastName = "Kowalsko",
                Email = ""
            });
    }

    private static void AddKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity => { entity.HasKey(e => e.IdProject).HasName("Project_pk"); });

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

        modelBuilder.Entity<TaskType>(entity => { entity.HasKey(e => e.IdTaskType).HasName("TaskType_pk"); });

        modelBuilder.Entity<TeamMember>(entity => { entity.HasKey(e => e.IdTeamMember).HasName("TeamMember_pk"); });
    }
}
