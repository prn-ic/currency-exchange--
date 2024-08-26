namespace CurrencyExchange.Core.Currencies;

public class CurrencyExchangeValue
{
    public int Id { get; set; }
    public Currency ExchangedCurrency { get; set; }
    public decimal Cost { get; set; }
    public CurrencyExchangeValue() { }
    public CurrencyExchangeValue(Currency currency, decimal cost) 
    {
        ArgumentNullException.ThrowIfNull(currency, nameof(currency));
        ExchangedCurrency = currency;
        Cost = cost;
    }
}