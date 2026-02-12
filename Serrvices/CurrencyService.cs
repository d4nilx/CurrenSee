using System.Net.Http.Json;
using CurrenSee.Models; 

namespace CurrenSee.Services; 

public class CurrencyService
{
    private const string apiKey = "YOUR_API_KEY";
    public const string BaseCurrency = "USD";
    
    private const string BaseUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{BaseCurrency}";
    
    private readonly HttpClient _httpClient;

    public CurrencyService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Dictionary<string, double>> GetRatesAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<CurrencyResponse>(BaseUrl);
            
            if (response != null && response.Result == "success")
            {
                return response.Rates;
            }

            return new Dictionary<string, double>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            return new Dictionary<string, double>();
        }
    }
}