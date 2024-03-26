using AwesomeShop.Services.Orders.Domain.Enums;
using AwesomeShop.Services.Orders.Domain.Events;
using AwesomeShop.Services.Orders.Domain.ValueObjects;

namespace AwesomeShop.Services.Orders.Domain.Entities
{
    public class Order : AggregateRoot
    {
        public Order(Customer customer, DeliveryAddress deliveryAddress, PaymentAddress paymentAddress, PaymentInfo paymentInfo, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            TotalPrice = items.Sum(i => i.Quantity * i.Price);
            Customer = customer;
            DeliveryAddress = deliveryAddress;
            PaymentAddress = paymentAddress;
            PaymentInfo = paymentInfo;
            CreatedAt = DateTime.Now;
            Status = OrderStatus.Started;
            Items = items;

            AddEvent(new OrderCreated(Id, TotalPrice, paymentInfo, Customer.FullName, Customer.Email));
        }

        public decimal TotalPrice { get; private set; }
        public Customer Customer { get; private set; }
        public DeliveryAddress DeliveryAddress { get; private set; }
        public PaymentAddress PaymentAddress { get; private set; }
        public PaymentInfo PaymentInfo { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }

        public void SetAsCompleted()
        {
            Status = OrderStatus.Completed;
        }

        public void SetAsRejected()
        {
            Status = OrderStatus.Rejected;
        }
    }
}
