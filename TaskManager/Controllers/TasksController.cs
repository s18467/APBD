using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dtos;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskMgrDbContext _context;

        public TasksController(TaskMgrDbContext context)
        {
            _context = context;
        }

        // GET: api/protasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks(int projectId)
        {
            return await _context.Tasks.ToListAsync(); //todo
        }

        // GET: api/pr/asks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/projects/2/tasks
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(int projectId, [FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (taskDto.Id != null)
            {
                return BadRequest("Id should be empty");
            }

            if (projectId != taskDto.IdTeam)
            {
                return BadRequest("Project id and team id do not match");
            }

            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                return BadRequest("Project does not exist");
            }

            var teamMember = await _context.TeamMembers.FindAsync(taskDto.IdAssignedTo);
            if (teamMember == null)
            {
                return BadRequest("Team member does not exist");
            }

            var creator = await _context.TeamMembers.FindAsync(taskDto.IdCreator);
            if (creator == null)
            {
                return BadRequest("Creator does not exist");
            }

            var taskTypeDto = taskDto.TaskType;
            var taskType = await _context.TaskTypes.FindAsync(taskTypeDto.IdTaskType);
            if (taskType == null)
            {
                var t = await _context.TaskTypes.AddAsync(new TaskType
                {
                    IdTaskType = taskTypeDto.IdTaskType,
                    Name = taskTypeDto.Name
                });
                await _context.SaveChangesAsync();
                taskType = t.Entity;
            }

            var task = new Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                Deadline = taskDto.Deadline,
                IdAssignedToNavigation = teamMember,
                IdCreatorNavigation = creator,
                IdTaskTypeNavigation = taskType,
                IdProjectNavigation = project,

            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { projectId = projectId, id = task.IdTask }, taskDto);
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.IdTask == id);
        }
    }
}
