using StudentVisor.Models;

namespace StudentVisor
{
    /// <summary>
    /// Implementation of IdataFileHandler interface
    /// </summary>
    /// <typeparam name="T">Extension of IEntity type</typeparam>
    public class DataFileHandler<T> : IDataFileHandler<T> where T : class, IEntity
    {
        public IDataParser<T> CsvParser { get; }
        private const string FilenamePattern = "data_{0}.{1}";
        private string FilePath { get; } //ToDo: proper name typeof
        private readonly object _lock = new();
        private static readonly List<Type> CreatedHdlrs = new();

        /// <summary>
        /// Creates Singleton with parsers to read/write data to/from file
        /// </summary>
        /// <param name="csvParser">Parser used to write data to file</param>
        public DataFileHandler(IDataParser<T> csvParser)
        {
            if (CreatedHdlrs.Contains(typeof(T)))
            {
                throw new ApplicationException($"DataFileHandler<{typeof(T).Name}> already exists");
            }
            CreatedHdlrs.Add(typeof(T));
            CsvParser = csvParser;
            var argType = typeof(T).Name.ToLower();
            var argFormat = CsvParser.Format.ToString().ToLower();
            FilePath = string.Format(FilenamePattern, argType, argFormat);
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        /// <inheritdoc />
        public void Write(List<T> entities)
        {
            var data = CsvParser.ConvertToStringFormat(entities);
            Write(data);
        }

        /// <inheritdoc />
        public void Write(string content)
        {
            lock (_lock)
            {
                using var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None);
                using var sw = new StreamWriter(fs);
                sw.WriteLine(content);
            }
        }

        /// <inheritdoc />
        public List<T> ReadAll()
        {
            string dataS;
            lock (_lock)
            {
                using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(fs);
                dataS = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(dataS))
            {
                return new List<T>();
            }

            var data = CsvParser.ConvertToList(dataS);
            return data;
        }

        /// <inheritdoc />
        public T ReadLine(string id)
        {
            string? selLine = null;
            lock (_lock)
            {
                using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    if (!line.Split(',')[2].Equals(id))
                    {
                        continue;
                    }
                    selLine = line;
                    break;
                }
            }

            if (selLine == null)
            {
                return null!;
            }
            var e = CsvParser.ConvertToObject(selLine);
            return e ?? throw new ArgumentException("Cannot convert to object");
        }

        /// <inheritdoc />
        public bool Exists(string id)
        {
            lock (_lock)
            {
                using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(fs);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) { continue; }

                    if (line.Split(',')[2].Equals(id)) { return true; }
                }
            }
            return false;
        }

        /// <inheritdoc />
        public void RemoveLine(string id)
        {
            lock (_lock)
            {
                var tempFile = Path.GetTempFileName();
                var newData = File.ReadLines(FilePath).Where(line => !line.Split(',')[2].Equals(id));

                File.WriteAllLines(tempFile, newData);
                File.Delete(FilePath);
                File.Move(tempFile, FilePath);
            }
        }

        /// <inheritdoc />
        public void Append(T entity)
        {
            lock (_lock)
            {
                var existing = File.ReadAllText(FilePath);
                var s = char.IsWhiteSpace(existing[^1]) ? "" : Environment.NewLine;
                s += CsvParser.ConvertToStringFormat(entity);
                File.AppendAllText(FilePath, s);
            }
        }
    }
}