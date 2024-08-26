using System.Linq.Expressions;

namespace CurrencyExchange.Core.Currencies;

public interface ICurrencyMarketService
{
    Task<IEnumerable<Currency>> GetAsync(
        Expression<Func<Currency, bool>> predicate,
        CancellationToken cancellationToken = default
    );
    Task<Currency> GetSingleAsync(
        Expression<Func<Currency, bool>> predicate,
        CancellationToken cancellationToken = default
    );
    Task AddCurrencyMarketAsync(
        int inner,
        int outer,
        decimal cost,
        CancellationToken cancellationToken = default
    );
    Task<Currency> AddAsync(Currency currency, CancellationToken cancellationToken = default);
    Task UpdateCostAsync(
        int id,
        int inner,
        decimal cost,
        CancellationToken cancellationToken = default
    );
    Task UpdateNameAsync(int id, string name, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
