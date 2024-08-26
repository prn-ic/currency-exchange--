using System.Linq.Expressions;
using CurrencyExchange.Core.Currencies;
using CurrencyExchange.Persistanse.Data;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistanse.Repositories;

public class CurrencyMarketRepository : ICurrencyMarketRepository
{
    private readonly AppDbContext _context;

    public CurrencyMarketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Currency> CreateAsync(
        Currency currency,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _context.Currencies.AddAsync(currency);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var currencyToRemove = await _context.Currencies.FirstAsync(
            x => x.Id == id,
            cancellationToken
        );
        _context.Currencies.Remove(currencyToRemove);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Currency>> FindAsync(
        Expression<Func<Currency, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        return await _context
            .Currencies.Where(predicate)
            .Include(x => x.AllowedToConvert)!
            .ThenInclude(s => s.ExchangedCurrency)
            .ThenInclude(s => s.AllowedToConvert)
            .ToListAsync(cancellationToken);
    }

    public async Task<Currency> UpdateAsync(
        Currency currency,
        CancellationToken cancellationToken = default
    )
    {
        var result = _context.Currencies.Update(currency);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}
