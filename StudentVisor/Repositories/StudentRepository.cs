using StudentVisor.Models;

namespace StudentVisor.Repositories
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(IDataFileHandler<Student> fileHandler) : base(fileHandler)
        {
        }
    }
}
