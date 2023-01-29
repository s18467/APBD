namespace StudentVisor.Models
{
    /// <summary>
    /// Class representing a student
    /// </summary>
    public class Student : IEntity
    {
        public string Id
        {
            get => IndexNumber;
            set => IndexNumber = value;
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IndexNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Studies { get; set; } = null!;
        public string Mode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FatherName { get; set; } = null!;
        public string MotherName { get; set; } = null!;

        public Student(string indexNumber)
        {
            IndexNumber = indexNumber;
        }

        public Student(string firstName, string lastName, string indexNumber, DateTime birthDate, string studies, string mode, string email, string fatherName, string motherName)
        {
            FirstName = firstName;
            LastName = lastName;
            IndexNumber = indexNumber;
            BirthDate = birthDate;
            Studies = studies;
            Mode = mode;
            Email = email;
            FatherName = fatherName;
            MotherName = motherName;
        }

        /// <summary>
        /// Checks if all fields are correct
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static bool Validate(Student student)
        {
            return !string.IsNullOrEmpty(student.FirstName) &&
                   !string.IsNullOrEmpty(student.LastName) &&
                   !string.IsNullOrEmpty(student.IndexNumber) &&
                   !string.IsNullOrEmpty(student.Studies) &&
                   !string.IsNullOrEmpty(student.Mode) &&
                   !string.IsNullOrEmpty(student.Email) &&
                   !string.IsNullOrEmpty(student.FatherName) &&
                   !string.IsNullOrEmpty(student.MotherName);
        }

        public override string ToString()
        {
            return $"{FirstName},{LastName},{IndexNumber},{BirthDate},{Studies},{Mode},{Email},{FatherName},{MotherName}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Student student && IndexNumber == student.IndexNumber;
        }

        protected bool Equals(Student other)
        {
            return IndexNumber == other.IndexNumber;
        }

        public override int GetHashCode()
        {
            return IndexNumber.GetHashCode();
        }
    }
}
