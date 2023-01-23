namespace CsvConverter
{
    public static class Log
    {
        private const string FileName = "log.txt";
        private static readonly object _lock = new object();

        public static void Initialize()
        {
            File.AppendAllText(FileName,
                $"{Environment.NewLine}{DateTime.Now:yyyy-MM-dd}{Environment.NewLine}============ Log started ============{Environment.NewLine}");
        }

        public static void ErrorAsync(string message)
        {
            Console.WriteLine(message);
            Write($"ERROR : {message}");
        }

        public static async void Write(string message)
        {
            Console.WriteLine(message);
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    using var streamWriter = new StreamWriter(FileName, true);
                    streamWriter.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {message} {Environment.NewLine}");
                }
            });
        }

        public static void Print(string message)
        {
            Console.WriteLine(message);
            //File.AppendAllText(FileName, $"{DateTime.Now.ToShortTimeString()} {message} {Environment.NewLine}");
        }
    }
}