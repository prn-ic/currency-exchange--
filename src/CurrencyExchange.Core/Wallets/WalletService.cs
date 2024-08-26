namespace CurrencyExchange.Core.Wallets;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    public WalletService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task<Wallet> CreateAsync(Wallet wallet, CancellationToken cancellationToken = default)
    {
        var result = await _walletRepository.CreateAsync(wallet, cancellationToken);
        return result;
    }


    public async Task<Wallet> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _walletRepository.FindAsync(x => x.Id == id, cancellationToken);
        return result.First();
    }

    public async Task UpdateAsync(Wallet wallet, CancellationToken cancellationToken = default)
    {
        await _walletRepository.UpdateAsync(wallet, cancellationToken); 
    }
}
