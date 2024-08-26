using System.Linq.Expressions;

namespace CurrencyExchange.Core.Currencies;

public interface ICurrencyMarketRepository
{
    Task<IEnumerable<Currency>> FindAsync(
        Expression<Func<Currency, bool>> predicate,
        CancellationToken cancellationToken = default
    );
    Task<Currency> CreateAsync(Currency currency, CancellationToken cancellationToken = default);
    Task<Currency> UpdateAsync(Currency currency, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
