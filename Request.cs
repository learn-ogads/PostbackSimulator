using Spectre.Console;

namespace OfferSimulation;

public class Request
{
    /// <summary>
    /// Performs a get request for the URL provided.
    /// </summary>
    public static async Task SendAsync(string url)
    {
        using var client = new HttpClient();
        AnsiConsole.MarkupLine($"[blue]Sending request: {url}[/]");
        try
        {
            await client.GetAsync(url);
            AnsiConsole.MarkupLine("[blue]Finished sending the request[/]");
        }
        catch (Exception)
        {
            AnsiConsole.MarkupLine("[red]Failed to send the request to the provided URL. Please check that your server or URL is active and valid![/]");
        }
    }
}