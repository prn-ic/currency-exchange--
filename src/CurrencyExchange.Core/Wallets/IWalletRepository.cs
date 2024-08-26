using System.Linq.Expressions;

namespace CurrencyExchange.Core.Wallets;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> FindAsync(Expression<Func<Wallet, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Wallet> CreateAsync(Wallet wallet, CancellationToken cancellationToken = default);    
    Task UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default);
}