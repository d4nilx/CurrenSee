using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CurrenSee.Models;
using CurrenSee.Services;

namespace CurrenSee.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly CurrencyService _service;
    private string _currentInput = "0"; 

    public ObservableCollection<CurrencyItem> Currencies { get; } = new();

    [ObservableProperty]
    private CurrencyItem _selectedCurrency; 

    public MainViewModel()
    {
        _service = new CurrencyService();
        LoadDataAsync();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        var rates = await _service.GetRatesAsync();
        Currencies.Clear();
        
        Currencies.Add(new CurrencyItem { Code = "PLN", Symbol = "zł", Rate = rates.GetValueOrDefault("PLN", 4.0), IsActive = true });
        Currencies.Add(new CurrencyItem { Code = "USD", Symbol = "$", Rate = 1.0 });
        Currencies.Add(new CurrencyItem { Code = "EUR", Symbol = "€", Rate = 0.92 });
        Currencies.Add(new CurrencyItem { Code = "UAH", Symbol = "₴", Rate = 41.5 });

        SelectedCurrency = Currencies.First();
        Recalculate();
    }
    
    [RelayCommand]
    private void SelectItem(CurrencyItem item)
    {
        foreach (var curr in Currencies) curr.IsActive = false;
        
        item.IsActive = true;
        SelectedCurrency = item;
        
        _currentInput = "0"; 
        Recalculate();
    }

    [RelayCommand]
    private void PressKey(string key)
    {
        if (key == "C") 
        {
            if (_currentInput.Length > 0)
                _currentInput = _currentInput.Remove(_currentInput.Length - 1);
            if (_currentInput == "") _currentInput = "0";
        }
        else if (key == ".")
        {
            if (!_currentInput.Contains(".")) _currentInput += ".";
        }
        else 
        {
            if (_currentInput == "0") _currentInput = key;
            else _currentInput += key;
        }

        Recalculate();
    }

    private void Recalculate()
    {
        if (SelectedCurrency == null) return;
        
        if (!decimal.TryParse(_currentInput, out decimal baseAmount)) return;

        SelectedCurrency.Amount = baseAmount;

        foreach (var item in Currencies)
        {
            if (item == SelectedCurrency) continue; 

            double result = ((double)baseAmount / SelectedCurrency.Rate) * item.Rate;
            item.Amount = (decimal)result;
        }
    }
}