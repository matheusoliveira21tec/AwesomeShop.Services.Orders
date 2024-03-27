using AwesomeShop.Services.Orders.Domain.Entities;

namespace AwesomeShop.Services.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}
