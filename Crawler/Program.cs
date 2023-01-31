using System.Text.RegularExpressions;

namespace Crawler
{
    public class Program
    {
        private const string urlPattern = @"^(http|https):\/\/[\w-]+(\.[\w-]+)+([\w.,@?^=%&amp;:\/~+#-]*[\w@?^=%&amp;\/~+#-])?$";
        private const string mailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

        static async Task Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                {
                    throw new ArgumentNullException("Please give URL as a parameter.");
                }

                var url = args[0];
                var urlMatch = Regex.IsMatch(url, @"^(http|https)://", RegexOptions.IgnoreCase);
                if (!urlMatch)
                {
                    throw new ArgumentException("Podaj poprawny URL");
                }

                Console.WriteLine("Prosze czekac ...");
                using var client = new HttpClient(); // Automatyczne uzycie Dispose() po zakonczeniu bloku using (automatycznie wykrywa)

                try
                {
                    var html = await client.GetStringAsync(url);
                    var emailMatches = Regex.Matches(html, mailPattern);

                    if (emailMatches.Count == 0)
                    {
                        Console.WriteLine("\nNie znaleziono adresów email.");
                        return;
                    }

                    var emails = emailMatches.Select(match => match.Value).Distinct().ToList();
                    Console.WriteLine($"\nEmail addresses found [{emails.Count}]:");
                    foreach (var email in emails)
                    {
                        Console.WriteLine(email);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Błąd w czasie pobierania strony");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}