namespace CsvConverter;

public class Student
{
    public string indexNumber { get; set; }
    public string fname { get; set; }
    public string lname { get; set; }
    public string birthdate { get; set; }
    public string email { get; set; }
    public string mothersName { get; set; }
    public string fathersName { get; set; }
    public Studies studies { get; set; }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Student student => Equals(student),
            _ => false
        };
    }

    protected bool Equals(Student other)
    {
        return indexNumber == other.indexNumber && fname == other.fname && lname == other.lname;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(indexNumber, fname, lname);
    }
}

public struct Studies
{
    public string name { get; set; }
    public string mode { get; set; }
}