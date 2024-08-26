using CurrencyExchange.Core.Wallets;
using CurrencyExchange.Core.Wallets.Events;
using MediatR;

namespace CurrencyExchange.Persistanse.DomainHandlers;

public class UpdateCostDomainEventHandler : INotificationHandler<UpdateCostDomainEvent>
{
    private readonly IWalletRepository _walletRepository;
    public UpdateCostDomainEventHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task Handle(UpdateCostDomainEvent notification, CancellationToken cancellationToken)
    {
        await _walletRepository.UpdateAsync(notification.Wallet, cancellationToken);
    }
}