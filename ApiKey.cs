using Spectre.Console;

namespace OfferSimulation;

public class ApiKey
{
    /// <summary>
    /// GetApiKey fetches the OGAds API key from the user and returns it
    /// </summary>
    public static string GetInput()
    {
        while (true)
        {
            Console.Write("Enter in API key:");
            var apiKey = Console.ReadLine();
            if (apiKey != null) return apiKey;
            AnsiConsole.MarkupLine("[red]Invalid API key provided[/]");
        }
    }
}