using CommunityToolkit.Mvvm.ComponentModel;

namespace CurrenSee.Models;

public partial class CurrencyItem : ObservableObject
{
    public string Code { get; set; } 
    public string Symbol { get; set; } 
    public double Rate { get; set; } 

    [ObservableProperty]
    private decimal _amount; 

    [ObservableProperty]
    private bool _isActive;
    
    public string Flag => GetFlagEmoji(Code.Substring(0, 2)); 

    private string GetFlagEmoji(string countryCode)
    {
        return string.Concat(countryCode.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
    }
}