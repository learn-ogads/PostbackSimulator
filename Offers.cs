using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using OfferSimulation.Core;
using OfferSimulation.Models;

namespace OfferSimulation;

public class Offers
{
    private readonly string _offerApi = "https://unlockcontent.net/api/v2";
    private readonly string _userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0.3 Mobile/15E148 Safari/604.1";

    /// <summary>
    /// Fetches offers from OGAds with the <c>_userAgent</c> and <c>IP address</c> provided.
    /// This allows us to send real offers to the user.
    /// </summary>
    public async Task<List<Offer>> GetAsync(string apiKey, string ipAddress)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        
        // Build URL with query parameters
        var builder = new UriBuilder(_offerApi);
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["ip"] = ipAddress;
        query["user_agent"] = _userAgent;
        query["ctype"] = "1";
        builder.Query = query.ToString();
        
        var url = builder.Uri.ToString();
        try
        {
            var resp = await client.GetStringAsync(url);
            var offers = JsonSerializer.Deserialize<OfferResponse>(resp);
            return offers!.Offers;
        }
        catch (Exception)
        {
            CustomConsole.WriteLine("[ERROR] Failed to fetch offers from OGAds. Either you provided an invalid API key or the request timed out", ConsoleColor.Red);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            throw;
        }
    }
}