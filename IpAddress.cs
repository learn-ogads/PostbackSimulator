using System.Text.Json;

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
        return json?["ip"];
    }
}