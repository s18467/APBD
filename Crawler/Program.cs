using System.Text.RegularExpressions;

namespace Crawler
{
    public class Program
    {
        //private const string url = "https://pastebin.com/archive";
        private const string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello! Looking for emails listed on provided URL. Please wait...");
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please give URL as a parameter.");
                return;
            }
            var url = args[0];
            using var client = new HttpClient();
            try
            {
                var html = await client.GetStringAsync(url);
                var emailMatches = Regex.Matches(html, pattern);

                if (emailMatches.Count == 0)
                {
                    Console.WriteLine("\nNo email addresses found.");
                    return;
                }

                Console.WriteLine($"\nEmail addresses found [{emailMatches.Count}]:");
                foreach (Match match in emailMatches)
                {
                    Console.WriteLine(match.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}