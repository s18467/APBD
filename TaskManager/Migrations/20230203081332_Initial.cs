using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    IdProject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deadline = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Project_pk", x => x.IdProject);
                });

            migrationBuilder.CreateTable(
                name: "TaskType",
                columns: table => new
                {
                    IdTaskType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TaskType_pk", x => x.IdTaskType);
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    IdTeamMember = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TeamMember_pk", x => x.IdTeamMember);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    IdTask = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deadline = table.Column<DateTime>(type: "date", nullable: false),
                    IdProject = table.Column<int>(type: "int", nullable: false),
                    IdTaskType = table.Column<int>(type: "int", nullable: false),
                    IdAssignedTo = table.Column<int>(type: "int", nullable: false),
                    IdCreator = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Task_pk", x => x.IdTask);
                    table.ForeignKey(
                        name: "Task_Project",
                        column: x => x.IdProject,
                        principalTable: "Project",
                        principalColumn: "IdProject");
                    table.ForeignKey(
                        name: "Task_TaskType",
                        column: x => x.IdTaskType,
                        principalTable: "TaskType",
                        principalColumn: "IdTaskType");
                    table.ForeignKey(
                        name: "Task_TeamMember1",
                        column: x => x.IdCreator,
                        principalTable: "TeamMember",
                        principalColumn: "IdTeamMember");
                    table.ForeignKey(
                        name: "Task_TeamMember2",
                        column: x => x.IdAssignedTo,
                        principalTable: "TeamMember",
                        principalColumn: "IdTeamMember");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdAssignedTo",
                table: "Task",
                column: "IdAssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdCreator",
                table: "Task",
                column: "IdCreator");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdProject",
                table: "Task",
                column: "IdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdTaskType",
                table: "Task",
                column: "IdTaskType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "TaskType");

            migrationBuilder.DropTable(
                name: "TeamMember");
        }
    }
}
