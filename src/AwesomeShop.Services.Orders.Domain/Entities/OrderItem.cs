namespace AwesomeShop.Services.Orders.Domain.Entities;

public class OrderItem : IEntityBase
{
    public OrderItem(Guid productId, int quantity, decimal price)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
}
