using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class TaskDto
    {
        public int? Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        //ToDO: Prawidlowy format daty - nie czyta
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Deadline { get; set; }

        [Required]
        public int IdTeam { get; set; }

        [Required]
        public int IdAssignedTo { get; set; }

        [Required]
        public int IdCreator { get; set; }

        [Required]
        public TaskTypeDto TaskType { get; set; }
    }
}
