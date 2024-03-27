using AwesomeShop.Services.Orders.Domain.Repositories;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus;

namespace AwesomeShop.Services.Orders.Application.UseCases.AddOrder;

public class AddOrderUseCase : IAddOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventProcessor _eventProcessor;
    public AddOrderUseCase(IOrderRepository orderRepository, IEventProcessor eventProcessor)
    {
        _orderRepository = orderRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task<AddOrderOutput> Execute(AddOrderInput input)
    {
        var order = input.ToEntity();

        await _orderRepository.AddAsync(order);

        _eventProcessor.Process(order.Events);

        return new AddOrderOutput(order.Id);
    }
}
