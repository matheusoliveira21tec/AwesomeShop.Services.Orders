using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System.Text;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus;

public class RabbitMqClient : IMessageBusClient
{
    private readonly IConnection _connection;
    public RabbitMqClient(ProducerConnection producerConnection)
    {
        _connection = producerConnection.Connection;
    }
    
    // Criado apenas para facilitar o desenvolvimento, em produção geralmente a 
    // equipe de infra coordenaria a criação e manutenção de Exchanges, Filas e Bindings.
    public static void Scaffold(IConnection connection) {
        var channel = connection.CreateModel();

        channel.ExchangeDeclare("order-service", "topic", true);
        channel.QueueDeclare("order-service/payment-accepted", true, false, false, null);
        
        channel.QueueBind("order-service/payment-accepted", "payment-service", "payment-accepted");
    }

    public void Publish(object message, string routingKey, string exchange)
    {
        var channel = _connection.CreateModel();

        var settings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var payload = JsonConvert.SerializeObject(message, settings);
        Console.WriteLine(payload);

        var body = Encoding.UTF8.GetBytes(payload);

        channel.BasicPublish(exchange, routingKey, null, body);
    }
}