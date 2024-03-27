namespace AwesomeShop.Services.Orders.Application.UseCases.AddOrder;

public interface IAddOrderUseCase
{
    Task<AddOrderOutput> Execute(AddOrderInput input);
}
