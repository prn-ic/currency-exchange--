using System.Linq.Expressions;
using CurrencyExchange.Core.Wallets;
using CurrencyExchange.Persistanse.Data;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistanse.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly AppDbContext _context;

    public WalletRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Wallet> CreateAsync(
        Wallet wallet,
        CancellationToken cancellationToken = default
    )
    {
        await _context.Wallets.AddAsync(wallet, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return wallet;
    }

    public async Task<IEnumerable<Wallet>> FindAsync(
        Expression<Func<Wallet, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Wallets.Where(predicate)
            .Include(x => x.Currency)
            .ThenInclude(x => x!.AllowedToConvert)!
            .ThenInclude(x => x!.ExchangedCurrency)
            .ThenInclude(s => s.AllowedToConvert)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default)
    {
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
