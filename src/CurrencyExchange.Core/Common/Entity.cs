using MediatR;

namespace CurrencyExchange.Core.Common;

public class Entity
{
    private List<INotification>? _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents =>
        _domainEvents?.AsReadOnly() ?? new List<INotification>().AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvent()
    {
        _domainEvents?.Clear();
    }
}
