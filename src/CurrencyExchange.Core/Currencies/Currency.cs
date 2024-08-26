using CurrencyExchange.Core.Common;
using CurrencyExchange.Core.Exceptions;

namespace CurrencyExchange.Core.Currencies;

public class Currency : Entity, IEquatable<Currency>
{
    public int Id { get; set; }
    private List<CurrencyExchangeValue>? _allowedToConvert;
    public string CurrencyName { get; set; }
    public IReadOnlyCollection<CurrencyExchangeValue>? AllowedToConvert => _allowedToConvert;

    public Currency() { }

    public Currency(string currencyName)
    {
        ArgumentException.ThrowIfNullOrEmpty(currencyName);

        CurrencyName = currencyName;

        _allowedToConvert = new List<CurrencyExchangeValue>();
    }

    public Currency(string currencyName, IEnumerable<CurrencyExchangeValue> currencies)
    {
        ArgumentException.ThrowIfNullOrEmpty(currencyName);

        CurrencyName = currencyName;

        _allowedToConvert = currencies.ToList() ?? new List<CurrencyExchangeValue>();
    }

    public decimal Convert(Currency currency, decimal capacity)
    {
        if (capacity == 0) capacity += 1;
        decimal firstCurrencyResult = 1;

        if (!AllowedToConvert.Any(x => x.ExchangedCurrency == currency))
        {
            Currency crossMarket = AllowedToConvert.First().ExchangedCurrency;

            firstCurrencyResult = crossMarket.Convert(currency, 1) * AllowedToConvert.First().Cost;
        }
        else
        {
            firstCurrencyResult = AllowedToConvert.First().Cost;
        }

        decimal number = firstCurrencyResult * capacity;
        return number;
    }

    public void SetAllowedToConvertCurrencies(IEnumerable<CurrencyExchangeValue> currencies)
    {
        _allowedToConvert = currencies.ToList() ?? new List<CurrencyExchangeValue>();
    }

    public void AddAllowedToConvertCurrencies(CurrencyExchangeValue currency)
    {
        _allowedToConvert?.Add(currency);
    }

    public void UpdateSingleAllowedCurrencyCost(int id, decimal cost)
    {
        var toUpdateCurrency = _allowedToConvert.First(x => x.ExchangedCurrency.Id == id);
        toUpdateCurrency.Cost = cost;
        _allowedToConvert.Remove(toUpdateCurrency);
        _allowedToConvert.Add(toUpdateCurrency);
    }

    public bool Equals(Currency? other)
    {
        if (other is null)
            return false;
        return CurrencyName.Equals(other.CurrencyName);
    }
}
