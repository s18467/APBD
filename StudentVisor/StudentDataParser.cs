using StudentVisor.Models;
using StudentVisor.Utils;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace StudentVisor
{
    public class StudentDataParser : IDataParser<Student>
    {
        /// <inheritdoc />
        public FormatType Format { get; set; } = FormatType.CSV;

        /// <inheritdoc />
        public string ConvertToStringFormat(Student s)
        {
            switch (Format)
            {
                case FormatType.CSV:
                    var sb = new StringBuilder();
                    sb.Append(s.FirstName).Append(",");
                    sb.Append(s.LastName).Append(",");
                    sb.Append(s.IndexNumber).Append(",");
                    var bd = s.BirthDate.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
                    sb.Append(bd).Append(",");
                    sb.Append(s.Studies).Append(",");
                    sb.Append(s.Mode).Append(",");
                    sb.Append(s.Email).Append(",");
                    sb.Append(s.FatherName).Append(",");
                    sb.Append(s.MotherName).Append(Environment.NewLine);
                    return sb.ToString();
                case FormatType.JSON:
                    return JsonSerializer.Serialize(s);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Format), Format, null);
            }
        }

        /// <inheritdoc />
        public Student? ConvertToObject(string dataString)
        {
            if (string.IsNullOrWhiteSpace(dataString))
            {
                return null;
            }
            switch (Format)
            {
                case FormatType.CSV:
                    var values = dataString.Split(',');
                    var student = new Student(values[2])
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        BirthDate = DateTime.ParseExact(values[3], "M/d/yyyy", CultureInfo.InvariantCulture),
                        Studies = values[4],
                        Mode = values[5],
                        Email = values[6],
                        FatherName = values[7],
                        MotherName = values[8]
                    };
                    return student;
                case FormatType.JSON:
                default:
                    throw new ArgumentOutOfRangeException(nameof(Format), Format, null);
            }
        }
    }
}
