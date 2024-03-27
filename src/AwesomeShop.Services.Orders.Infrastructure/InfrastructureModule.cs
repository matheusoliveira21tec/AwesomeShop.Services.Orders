using AwesomeShop.Services.Orders.Domain.Repositories;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus;
using AwesomeShop.Services.Orders.Infrastructure.Persistence;
using AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Orders.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
            services
                .AddMongo()
                .AddRepositories()
                .AddRabbitMq();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services) {
            services.AddSingleton(sp => {
                var configuration = sp.GetService<IConfiguration>();
                var options = new MongoDbOptions();

                configuration.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(sp => {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp => {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                
                var options = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<IMongoClient>();

                return mongoClient.GetDatabase(options.Database);
            });

            return services;
        }
        private static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {


            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();

                var options = new RabbitMQOptions();

                configuration.GetSection("RabbitMQ").Bind(options);

                Console.WriteLine(options.Hostname);

                var connectionFactory = new ConnectionFactory
                {
                    HostName = options.Hostname
                };

                var connection = connectionFactory.CreateConnection("order-service-producer");
                var producerConnection = new ProducerConnection(connection);

                RabbitMqClient.Scaffold(producerConnection.Connection);

                return producerConnection;
            });

            services.AddScoped<IMessageBusClient, RabbitMqClient>();
            services.AddTransient<IEventProcessor, EventProcessor>();


            return services;
        }

    }
}