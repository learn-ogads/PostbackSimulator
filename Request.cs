using OfferSimulation.Core;

namespace OfferSimulation;

public class Request
{
    /// <summary>
    /// Performs a get request for the URL provided.
    /// </summary>
    public static async Task SendAsync(string url)
    {
        using var client = new HttpClient();
        CustomConsole.WriteLine($"[INFO] Sending request: {url}", ConsoleColor.Blue);
        await client.GetAsync(url);
        CustomConsole.WriteLine("[INFO] Finished sending the request", ConsoleColor.Blue);
    }
}