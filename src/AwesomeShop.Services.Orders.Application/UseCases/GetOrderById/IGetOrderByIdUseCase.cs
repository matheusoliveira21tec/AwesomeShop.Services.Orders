namespace AwesomeShop.Services.Orders.Application.UseCases.GetOrderById;

public interface IGetOrderByIdUseCase
{
    Task<GetOrderByIdOutput> Execute(Guid id);
}