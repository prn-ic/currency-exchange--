using CurrencyExchange.Core.Currencies;

namespace CurrencyExchange.Core.Wallets;

public interface IWalletService
{
    Task<Wallet> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Wallet> CreateAsync(Wallet wallet, CancellationToken cancellationToken = default);
    Task UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default);
}