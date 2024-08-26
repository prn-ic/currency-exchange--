using CurrencyExchange.Core.Wallets.Events;
using MediatR;

namespace CurrencyExchange.Persistanse.DomainHandlers;

public class SomethingHandlerEvent : INotificationHandler<UpdateCostDomainEvent>
{
    public Task Handle(UpdateCostDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("AAAAAAAAAAAAAAAAAAAA COST UPDATEDDDD IN YOUR WALLLET");
        return Task.CompletedTask;
    }
}