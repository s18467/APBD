using System.ComponentModel.DataAnnotations;

namespace Students.Data
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime BirthDate { get; set; }
        public string Studies { get; set; }
    }
}