using CurrencyExchange.Core.Wallets;
using CurrencyExchange.Core.Wallets.Events;
using MediatR;

namespace CurrencyExchange.WebApi.DomainHandlers;

public class UpdateCostDomainEventHandler : INotificationHandler<UpdateCostDomainEvent>
{
    private readonly IWalletService _walletService;

    public UpdateCostDomainEventHandler(IWalletService walletService)
    {
        _walletService = walletService;
    }

    public async Task Handle(
        UpdateCostDomainEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _walletService.UpdateAsync(notification.Wallet, cancellationToken);
    }
}
