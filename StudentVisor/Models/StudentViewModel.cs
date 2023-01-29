using System.ComponentModel.DataAnnotations;

namespace StudentVisor.Models
{
    /// <summary>
    /// ViewModel for Student model in MVVM pattern
    /// </summary>
    public class StudentViewModel
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Key, RegularExpression(@"^s[0-9]{1,8}$")]
        public string Index { get; set; } = null!;

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Studies { get; set; } = null!;

        [Required]
        public string Mode { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string FatherName { get; set; } = null!;

        [Required]
        public string MotherName { get; set; } = null!;

        public StudentViewModel()
        {
        }

        /// <summary>
        /// Converts Student model to StudentViewModel
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static StudentViewModel ToViewModel(Student student)
        {
            return new StudentViewModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Index = student.IndexNumber,
                BirthDate = student.BirthDate,
                Studies = student.Studies,
                Mode = student.Mode,
                Email = student.Email,
                FatherName = student.FatherName,
                MotherName = student.MotherName
            };
        }

        /// <summary>
        /// Converts StudentViewModel to Student model and auto validates it
        /// </summary>
        /// <param name="svm"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Student ToModel(StudentViewModel svm)
        {
            var student = new Student(svm.FirstName, svm.LastName, svm.Index, svm.BirthDate, svm.Studies, svm.Mode, svm.Email, svm.FatherName, svm.MotherName);
            return Student.Validate(student) ? student : throw new ArgumentException("Invalid student data");
        }
    }
}
