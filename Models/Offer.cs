using System.Text.Json.Serialization;

namespace OfferSimulation.Models;

public class Offer
{
    [JsonPropertyName("offerid")]
    public int OfferId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    [JsonPropertyName("name_short")]
    public string NameShort { get; set; } = default!;
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
    [JsonPropertyName("adcopy")]
    public string AdCopy { get; set; } = default!;
    [JsonPropertyName("picture")]
    public string Picture { get; set; } = default!;
    [JsonPropertyName("payout")]
    public string Payout { get; set; } = default!;
    [JsonPropertyName("country")]
    public string Country { get; set; } = default!;
    [JsonPropertyName("device")]
    public string Device { get; set; } = default!;
    [JsonPropertyName("link")]
    public string Link { get; set; } = default!;
    [JsonPropertyName("epc")]
    public string Epc { get; set; } = default!;

    public override string ToString()
    {
        return NameShort;
    }
}