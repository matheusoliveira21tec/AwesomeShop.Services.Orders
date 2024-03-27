using AwesomeShop.Services.Orders.Domain.Repositories;

namespace AwesomeShop.Services.Orders.Application.UseCases.GetOrderById;

public class GetOrderByIdUseCase : IGetOrderByIdUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderByIdOutput> Execute(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        var output = GetOrderByIdOutput.FromEntity(order);

        return output;
    }
}