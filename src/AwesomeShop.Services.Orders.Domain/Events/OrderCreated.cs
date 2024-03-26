using AwesomeShop.Services.Orders.Domain.ValueObjects;

namespace AwesomeShop.Services.Orders.Domain.Events;

public class OrderCreated : IDomainEvent
{
    public OrderCreated(Guid id, decimal totalPrice, PaymentInfo paymentInfo, string fullName, string email)
    {
        Id = id;
        TotalPrice = totalPrice;
        PaymentInfo = paymentInfo;
        FullName = fullName;
        Email = email;
    }

    public Guid Id { get; private set; }
    public decimal TotalPrice { get; private set; }
    public PaymentInfo PaymentInfo { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
}
