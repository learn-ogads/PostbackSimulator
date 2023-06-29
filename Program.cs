using OfferSimulation.Models;
using Spectre.Console;

namespace OfferSimulation;

public class Program
{
    private readonly string _version = "1.0.1";

    public static Task Main(string[] args) => new Program().RunAsync();

    private async Task RunAsync()
    {
        AnsiConsole.MarkupLine($"[blue]OGAds Postback Simulator V{_version}[/]");
        AnsiConsole.MarkupLine("[yellow]Example URL: http://localhost:8000/postback?id={offer_id}&payout={payout}&ip={session_ip}[/]");
        var random = new Random();
        var (url,ipAddress, offers) = await GetInputAndOffersAsync();

        while (true)
        {
            Console.Write("Send a request? (y/n): ");
            var key = Console.ReadKey();
            Console.WriteLine();
            
            // Check if an invalid key was provided
            if (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N)
            {
                AnsiConsole.MarkupLine("[yellow]Invalid option selected, please try again[/]");
                continue;
            }

            if (key.Key == ConsoleKey.N)
            {
                AnsiConsole.MarkupLine("[red]Quiting the application...[/]");
                Environment.Exit(0);
            }
            
            var offer = offers[random.Next(offers.Count)]; // Get random offer
            var postbackUrl = new PostbackUrl(url, offer, ipAddress);
            var convertedUrl = postbackUrl.ReplaceAll();
            await Request.SendAsync(convertedUrl);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static async Task<(string url, string ipAddress, List<Offer> offers)> GetInputAndOffersAsync()
    {
        var apiKey = ApiKey.GetInput();
        var url = PostbackUrl.GetInput();

        var offers = new Offers();
        var ipAddress = await IpAddress.GetAsync();
        if (ipAddress == null)
        {
            AnsiConsole.MarkupLine("[red]Failed to get your ip address[/]");
            Environment.Exit(1);
        }
        
        var offerResp = await offers.GetAsync(apiKey, ipAddress);
        return (url, ipAddress, offerResp);
    }
}