namespace TaskManager.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}
