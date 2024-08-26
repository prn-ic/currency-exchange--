using CurrencyExchange.Core.Common;
using CurrencyExchange.Core.Currencies;
using CurrencyExchange.Core.Wallets.Events;

namespace CurrencyExchange.Core.Wallets;

public class Wallet : Entity
{
    public int Id { get; set; }
    public decimal Capacity { get; set; }
    public Currency? Currency { get; set; }
    public int CurrencyId { get; set; }

    public Wallet() { }

    public Wallet(decimal capacity, Currency currency)
    {
        ArgumentNullException.ThrowIfNull(currency);

        Capacity = capacity;
        Currency = currency;
    }

    public void Descrease(decimal deductible, Currency deductibleCurrency)
    {
        if (Currency!.Equals(deductibleCurrency))
            Capacity = Capacity - deductible;
        else
            Capacity = Currency.Convert(deductibleCurrency, deductible) - Capacity;

        AddDomainEvent(new UpdateCostDomainEvent(this));
    }

    public void Increase(decimal addend, Currency addendCurrency)
    {
        if (Currency!.Equals(addendCurrency))
            Capacity = Capacity + addend;
        else
            Capacity = Currency.Convert(addendCurrency, addend) + Capacity;
            
        AddDomainEvent(new UpdateCostDomainEvent(this));
    }
}
