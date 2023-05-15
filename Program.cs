using System.Diagnostics;
using System.Text.RegularExpressions;
using Bogus;
using OfferSimulation;
using OfferSimulation.Core;

public class Program
{
    private readonly string _version = "1.0.0";

    public static Task Main(string[] args) => new Program().RunAsync();

    private async Task RunAsync()
    {
        CustomConsole.WriteLine($"[INFO] OGAds Postback Simulator V{_version}", ConsoleColor.Green);
        CustomConsole.WriteLine("[INFO] Example URL: http://localhost:8000/postback?id={offer_id}&payout={payout}&ip={session_ip}", ConsoleColor.Yellow);
        var apiKey = GetApiKey();
        var url = GetUrl();
        var random = new Random();
        var faker = new Faker();

        var offers = new Offers();
        var ipAddress = await IpAddress.GetAsync();
        if (ipAddress == null)
        {
            Console.WriteLine("Failed to get your ip address");
            return;
        }
        Debug.WriteLine($"Users ip address is: {ipAddress}"); 
        var offerResp = await offers.GetAsync(apiKey, ipAddress);

        while (true)
        {
            Console.Write("Send a request? (y/n): ");
            var key = Console.ReadKey();
            Console.Write("\n");
            
            // Check if an invalid key was provided
            if (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N)
            {
                Console.WriteLine("Invalid option selected, please try again");
                continue;
            }

            if (key.Key == ConsoleKey.N)
            {
                CustomConsole.WriteLine("[INFO] Quiting the application...", ConsoleColor.Red);
                Environment.Exit(1);
            }
            
            var offer = offerResp[random.Next(offerResp.Count)]; // Get random offer
            var postbackUrl = new PostbackUrl(url, offer, ipAddress);
            var convertedUrl = postbackUrl.ReplaceAll();
            await Request.SendAsync(convertedUrl);
        }
    }

    private string GetApiKey()
    {
        while (true)
        {
            Console.Write("Enter in API key:");
            var apiKey = Console.ReadLine();
            if (apiKey != null) return apiKey;
            Console.WriteLine("Invalid API key provided");
        }
    }

    private string GetUrl()
    {
        while (true)
        {
            Console.Write("Enter in your URL:");
            var url = Console.ReadLine();
            Debug.WriteLine($"URL provided was: {url}");

            if (url != null && ValidateUrl(url)) return url;
            Console.WriteLine("Invalid URL provided! Please try again");
        }
    }

    private bool ValidateUrl(string url)
    {
        return url.StartsWith("https://www.") || url.StartsWith("http://www.") || url.StartsWith("http://") ||
               url.StartsWith("https://");
    }
}
