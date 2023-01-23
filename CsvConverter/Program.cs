namespace CsvConverter
{
    public class Program
    {
        private static string _inputFile;
        private static string _outputLocation;
        private static string _format;

        static void Main(string[] args)
        {
            if (!TryParseArgs(args))
            {
                return;
            }

        }

        private static bool TryParseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: CsvToXmlConverter.exe <input csv file> <output location> <format>");
                return false;
            }
            _inputFile = args[0];
            _outputLocation = args[1];
            _format = args[2];

            if (!File.Exists(_inputFile))
            {
                Console.WriteLine("Input file does not exist");
                return false;
            }
            if (!Directory.Exists(_outputLocation))
            {
                Console.WriteLine("Output location does not exist");
                return false;
            }
            if (_format != "xml" && _format != "json")
            {
                Console.WriteLine("Format must be xml or json");
                return false;
            }

            return true;
        }
    }
}