using AwesomeShop.Services.Orders.Domain.Events;
using System.Text;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus;

public class EventProcessor : IEventProcessor
{
    private readonly IMessageBusClient _bus;
    public EventProcessor(IMessageBusClient bus)
    {
        _bus = bus;
    }
    
    public void Process(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events) {
            var routingKey = ToDashCase(@event.GetType().Name);

            _bus.Publish(@event, routingKey, "order-service");
        }
    }

    public string ToDashCase(string input) {
        var sb = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        if (i != 0 && char.IsUpper(input[i]))
            sb.Append($"-{input[i]}");
        else
            sb.Append(input[i]);

        return sb.ToString().ToLower();
    }
}