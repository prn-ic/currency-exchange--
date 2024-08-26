using System.Linq.Expressions;

namespace CurrencyExchange.Core.Currencies;

public class CurrencyMarketService : ICurrencyMarketService
{
    private readonly ICurrencyMarketRepository _currencyMarketRepository;

    public CurrencyMarketService(ICurrencyMarketRepository currencyMarketRepository)
    {
        _currencyMarketRepository = currencyMarketRepository;
    }

    public async Task<Currency> AddAsync(
        Currency currency,
        CancellationToken cancellationToken = default
    )
    {
        Currency result = await _currencyMarketRepository.CreateAsync(currency, cancellationToken);
        return result;
    }

    public async Task AddCurrencyMarketAsync(int inner, int outer, decimal cost, CancellationToken cancellationToken = default)
    {
        Currency innerCurrency = (await _currencyMarketRepository.FindAsync(x => x.Id == inner, cancellationToken)).First();
        Currency outerCurrency = (await _currencyMarketRepository.FindAsync(x => x.Id == outer, cancellationToken)).First();

        innerCurrency.AddAllowedToConvertCurrencies(new(outerCurrency, cost));
        await _currencyMarketRepository.UpdateAsync(innerCurrency, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _currencyMarketRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Currency>> GetAsync(
        Expression<Func<Currency, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        return await _currencyMarketRepository.FindAsync(predicate, cancellationToken);
    }

    public async Task<Currency> GetSingleAsync(
        Expression<Func<Currency, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _currencyMarketRepository.FindAsync(predicate, cancellationToken);
        return result.First();
    }

    public async Task UpdateCostAsync(
        int id,
        int inner,
        decimal cost,
        CancellationToken cancellationToken = default
    )
    {
        Currency updatableCurrency = (
            await _currencyMarketRepository.FindAsync(x => x.Id == id, cancellationToken)
        ).First();

        updatableCurrency.UpdateSingleAllowedCurrencyCost(inner, cost);
        var result = await _currencyMarketRepository.UpdateAsync(
            updatableCurrency,
            cancellationToken
        );
    }

    public async Task UpdateNameAsync(int id, string name, CancellationToken cancellationToken = default)
    {
        Currency updatableCurrency = (
            await _currencyMarketRepository.FindAsync(x => x.Id == id, cancellationToken)
        ).First();

        updatableCurrency.CurrencyName = name;
        await _currencyMarketRepository.UpdateAsync(updatableCurrency, cancellationToken);
    }
}
