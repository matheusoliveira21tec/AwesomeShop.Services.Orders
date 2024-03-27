using AwesomeShop.Services.Orders.Domain.Repositories;

namespace AwesomeShop.Services.Orders.Application.UseCases.AddOrder;

public class AddOrderUseCase : IAddOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    public AddOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<AddOrderOutput> Execute(AddOrderInput input)
    {
        var order = input.ToEntity();

        await _orderRepository.AddAsync(order);


        return new AddOrderOutput(order.Id);
    }
}
