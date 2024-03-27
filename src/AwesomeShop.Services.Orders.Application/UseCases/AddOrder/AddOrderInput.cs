using AwesomeShop.Services.Orders.Domain.Entities;
using AwesomeShop.Services.Orders.Domain.ValueObjects;

namespace AwesomeShop.Services.Orders.Application.UseCases.AddOrder;

public class AddOrderInput
{
    public CustomerInput Customer { get; set; }
    public List<OrderItemInput> Items { get; set; }
    public DeliveryAddressInput DeliveryAddress { get; set; }
    public PaymentAddressInput PaymentAddress { get; set; }
    public PaymentInfoInput PaymentInfo { get; set; }

    public Order ToEntity()
    {
        return new Order(
            new Customer(Customer.Id, Customer.FullName, Customer.Email),
            new DeliveryAddress(DeliveryAddress.Street, DeliveryAddress.Number, DeliveryAddress.City, DeliveryAddress.State, DeliveryAddress.ZipCode),
            new PaymentAddress(PaymentAddress.Street, PaymentAddress.Number, PaymentAddress.City, PaymentAddress.State, PaymentAddress.ZipCode),
            new PaymentInfo(PaymentInfo.CardNumber, PaymentInfo.FullName, PaymentInfo.ExpirationDate, PaymentInfo.Cvv),
            Items.Select(i => new OrderItem(i.ProductId, i.Quantity, i.Price)).ToList()
        );
    }
}

public class CustomerInput
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public class OrderItemInput
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class DeliveryAddressInput
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PaymentAddressInput
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PaymentInfoInput
{
    public string CardNumber { get; set; }
    public string FullName { get; set; }
    public string ExpirationDate { get; set; }
    public string Cvv { get; set; }
}