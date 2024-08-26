using MediatR;

namespace CurrencyExchange.Core.Wallets.Events;

public class UpdateCostDomainEvent : INotification
{
    public Wallet Wallet { get; }

    public UpdateCostDomainEvent(Wallet wallet)
    {
        Wallet = wallet;
    }
}