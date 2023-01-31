using System.Text.Json;

namespace CsvConverter
{
    public class Program
    {
        private static string _inputFile;
        private static string _outputLocation;
        private static string _format;
        private static List<dynamic> _data;

        static async Task Main(string[] args)
        {
            Log.Initialize();
            try
            {
                ParseArgs(args);
                await Convert();
            }
            catch (Exception e)
            {
                Log.ErrorAsync(e.Message);
            }

            Log.Write("\n----- Finished ------");
        }

        private static void ParseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                Log.ErrorAsync("Usage: CsvToXmlConverter.exe <input csv file> <output location> <format>");
            }

            _inputFile = args[0];
            _outputLocation = args[1];
            _format = args[2];

            if (!File.Exists(_inputFile))
            {
                throw new FileNotFoundException($"Plik '{_inputFile}' nie istnieje!");
            }

            if (!Directory.Exists(_outputLocation))
            {
                throw new FileNotFoundException($"Folder '{_outputLocation}' nie istnieje!");
            }

            if (_format != "xml" && _format != "json")
            {
                throw new ArgumentException("Format musi być 'xml' lub 'json'");
            }

            //TODO: Implement xml format
            if (_format == "xml")
            {
                throw new NotImplementedException("Format 'xml' jeszcze nie zaimplementowany.");
            }
        }

        private static async Task Convert()
        {
            Log.Print("Reading file...");
            await using (var stream = File.OpenRead(_inputFile))
            {
                using (var reader = new StreamReader(stream))
                {
                    _data = new List<dynamic>();
                    var tasks = new List<Task>();
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        tasks.Add(ConvertLineAsync(line));
                    }

                    Log.Print($"Converting {_data.Count} ...");
                    Task.WaitAll(tasks.ToArray());
                }
            }

            Log.Print("Writing target file ...");
            var json = JsonSerializer.Serialize(new
            {
                uczelnia = new
                {
                    utworzony = DateTime.Now,
                    studenci = _data
                }
            });
            await File.WriteAllTextAsync(
                Path.Combine(_outputLocation, Path.GetFileNameWithoutExtension(_inputFile) + '.' + _format), json);
        }

        private static async Task ConvertLineAsync(string? line)
        {
            await Task.Run(() => ConvertLine(line));
        }

        private static void ConvertLine(string? line)
        {
            try
            {
                if (line == null || line.Trim().Length == 0)
                {
                    throw new Exception("W pliku wystepuje pusta linia");
                }

                var values = line.Split(',').Select(v =>
                {
                    if ((v = v.Trim()).Length == 0)
                    {
                        throw new Exception($"Pusta kolumna w linii:\n{line}");
                    }

                    return v.Trim();
                }).ToArray();
                //Log.Print($"{values.Length}: {line}");
                if (values.Length != 9)
                {
                    throw new Exception($"Zła ilość pól [{values.Length}/9]:\n{line}");
                }

                var indexNumber = values[4];
                var fname = values[0];
                var lname = values[1];
                var birthdate = values[5];
                var email = values[6];
                var mothersName = values[7];
                var fathersName = values[8];
                var studiesName = values[2];
                var studiesMode = values[3];

                var student = new Student
                {
                    indexNumber = indexNumber,
                    fname = fname,
                    lname = lname,
                    birthdate = birthdate,
                    email = email,
                    mothersName = mothersName,
                    fathersName = fathersName,
                    studies = new Studies
                    {
                        name = studiesName,
                        mode = studiesMode
                    }
                };

                if (_data.Contains(student))
                {
                    throw new Exception($"Zduplikowany student:\n{line}");
                }
                _data.Add(student);
            }
            catch (Exception e)
            {
                Log.ErrorAsync(e.Message);
            }
        }
    }
}