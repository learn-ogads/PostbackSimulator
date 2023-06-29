using System.Text.Json.Serialization;

namespace OfferSimulation.Models;

public class OfferResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("error")]
    public object Error { get; set; } = default!;
    [JsonPropertyName("offers")]
    public List<Offer> Offers { get; set; } = default!;
}