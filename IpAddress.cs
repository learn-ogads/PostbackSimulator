using System.Text.Json;
using Spectre.Console;

namespace OfferSimulation;

public static class IpAddress
{
    /// <summary>
    /// Gets the current users IP Address.
    /// This IP is used for fetching offers from OGAds.
    /// </summary>
    public static async Task<string?> GetAsync()
    {
        using var client = new HttpClient();
        var resp = await client.GetStringAsync("https://api.ipify.org/?format=json");
        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(resp);
        if (json != null) return json["ip"];
        AnsiConsole.MarkupLine("[red]Failed to decode the json for your IP address[/]");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Environment.Exit(1);
        throw new Exception("Failed to decode the json for your IP address");
    }
}