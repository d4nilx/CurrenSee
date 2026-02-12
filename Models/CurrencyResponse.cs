using System.Text.Json.Serialization;

namespace CurrenSee.Models;

public class CurrencyResponse
{
    [JsonPropertyName("result")]
    public string Result { get; set; } 

    [JsonPropertyName("base_code")]
    public string BaseCode { get; set; }

    [JsonPropertyName("conversion_rates")] 
    public Dictionary<string, double> Rates { get; set; }
}