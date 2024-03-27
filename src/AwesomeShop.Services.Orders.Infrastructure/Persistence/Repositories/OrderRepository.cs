using AwesomeShop.Services.Orders.Domain.Entities;
using AwesomeShop.Services.Orders.Domain.Repositories;
using MongoDB.Driver;

namespace AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;
    public OrderRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Order>("orders");
    }

    public async Task AddAsync(Order order)
    {
        await _collection.InsertOneAsync(order);
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        return await _collection.Find(c => c.Id == id).SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        await _collection.ReplaceOneAsync(o => o.Id == order.Id, order);
    }

}