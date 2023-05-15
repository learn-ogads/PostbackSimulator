using System.Text.Json.Serialization;

namespace OfferSimulation.Models;

public class Offer
{
    [JsonPropertyName("offerid")]
    public int OfferId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("name_short")]
    public string NameShort { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("adcopy")]
    public string AdCopy { get; set; }
    [JsonPropertyName("picture")]
    public string Picture { get; set; }
    [JsonPropertyName("payout")]
    public string Payout { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
    [JsonPropertyName("device")]
    public string Device { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
    [JsonPropertyName("epc")]
    public string Epc { get; set; }

    public override string ToString()
    {
        return NameShort;
    }
}