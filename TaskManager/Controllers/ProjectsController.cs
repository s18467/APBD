using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly TaskMgrDbContext _context;

        public ProjectsController(TaskMgrDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            if (!ProjectExists(id))
            {
                return NotFound();
            }

            var project = await _context.Projects.Where(p => p.IdProject == id).Include(p => p.Tasks)
                .ThenInclude(t => t.IdTaskTypeNavigation).FirstAsync();

            var dto = new ProjectDto
            {
                Id = id,
                Name = project.Name,
                Tasks = project.Tasks.OrderByDescending(t => t.Deadline).Select(t => new SimpleTaskDto
                {
                    Id = t.IdTask,
                    Name = t.Name,
                    Description = t.Description,
                    Deadline = t.Deadline,
                    TaskType = new TaskTypeDto
                    {
                        IdTaskType = t.IdTaskType,
                        Name = t.IdTaskTypeNavigation.Name
                    }
                }).ToList()
            };

            return dto;
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.IdProject == id);
        }
    }
}