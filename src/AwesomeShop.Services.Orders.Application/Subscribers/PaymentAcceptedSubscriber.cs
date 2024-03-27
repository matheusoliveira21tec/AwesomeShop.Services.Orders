using AwesomeShop.Services.Orders.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AwesomeShop.Services.Orders.Application.Subscribers;

public class PaymentAcceptedSubscriber : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string Queue = "order-service/payment-accepted";
    private const string Exchange = "order-service";
    private const string RoutingKey = "payment-accepted";
    public PaymentAcceptedSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var connectionFactory = new ConnectionFactory
        {
            HostName = serviceProvider.GetService<IConfiguration>().GetSection("RabbitMQ").GetValue<string>("Hostname")
        };

        for (int tries = 1; tries <= 3; tries++)
        {
            try
            {
                _connection = connectionFactory.CreateConnection("order-service-payment-accepted-consumer");
                _channel = _connection.CreateModel();
                break;
            }
            catch (Exception)
            {
                Thread.Sleep(30000);
            }
        }
        if (_channel == null)
            throw new InvalidOperationException("Channel could not be created");

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonConvert.DeserializeObject<PaymentAccepted>(contentString);

            Console.WriteLine($"Message PaymentAccepted received with Id {message.Id}");

            await UpdateOrder(message);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }

    private async Task<bool> UpdateOrder(PaymentAccepted payment)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var orderRepository = scope.ServiceProvider.GetService<IOrderRepository>();

            var order = await orderRepository.GetByIdAsync(payment.Id);

            order.SetAsCompleted();

            await orderRepository.UpdateAsync(order);

            return true;
        }
    }
}

public class PaymentAccepted
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}