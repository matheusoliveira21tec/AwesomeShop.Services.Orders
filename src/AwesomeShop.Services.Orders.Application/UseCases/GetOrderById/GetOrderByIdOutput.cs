using AwesomeShop.Services.Orders.Domain.Entities;

namespace AwesomeShop.Services.Orders.Application.UseCases.GetOrderById;

public class GetOrderByIdOutput
{
    public GetOrderByIdOutput(Guid id, decimal totalPrice, DateTime createdAt, string status)
    {
        Id = id;
        TotalPrice = totalPrice;
        CreatedAt = createdAt;
        Status = status;
    }

    public Guid Id { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Status { get; private set; }

    public static GetOrderByIdOutput FromEntity(Order order) {
        return new GetOrderByIdOutput(order.Id, order.TotalPrice, order.CreatedAt, order.Status.ToString());
    }
}