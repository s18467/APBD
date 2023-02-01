namespace Students.Data
{
    public class StudentService
    {
        private static readonly List<Student> Students = new()
        {
            new Student { Id = 1, FirstName = "John", LastName = "Doe", Avatar = "https://cdn-icons-png.flaticon.com/128/4333/4333609.png", BirthDate = new DateTime(1980, 1, 1), Studies = "Computer Science", Email = "john.doe@example.com" },
            new Student { Id = 2, FirstName = "Jane", LastName = "Doe", Avatar = "https://cdn-icons-png.flaticon.com/128/236/236832.png", BirthDate = new DateTime(1981, 2, 2), Studies = "Mathematics", Email = "jane.doe@example.com" },
            new Student { Id = 3, FirstName = "Bob", LastName = "Smith", Avatar = "https://cdn-icons-png.flaticon.com/128/456/456212.png", BirthDate = new DateTime(1982, 3, 3), Studies = "Physics", Email = "bob.smith@example.com" }
        };

        public Task<Student[]> GetStudents()
        {
            return Task.FromResult(Students.ToArray());
        }

        public static Student GetStudent(int id)
        {
            var student = Students.First(s => s.Id == id);
            return student;
        }

        public Task DeleteStudentAsync(int id)
        {
            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                Students.Remove(student);
            }
            return Task.CompletedTask;
        }
    }
}