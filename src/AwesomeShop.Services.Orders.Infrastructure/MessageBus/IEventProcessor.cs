using AwesomeShop.Services.Orders.Domain.Events;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus;

public interface IEventProcessor
{
    void Process(IEnumerable<IDomainEvent> events);
}