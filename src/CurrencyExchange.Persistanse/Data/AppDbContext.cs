using CurrencyExchange.Core.Currencies;
using CurrencyExchange.Core.Wallets;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistanse.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Currency> Currencies => Set<Currency>();
    public virtual DbSet<Wallet> Wallets => Set<Wallet>();

    public AppDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Currency>().HasMany(x => x.AllowedToConvert).WithOne();
    }
}
