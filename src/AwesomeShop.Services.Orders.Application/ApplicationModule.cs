using AwesomeShop.Services.Orders.Application.Subscribers;
using AwesomeShop.Services.Orders.Application.UseCases.AddOrder;
using AwesomeShop.Services.Orders.Application.UseCases.GetOrderById;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Orders.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddSubscribers();

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddOrderUseCase, AddOrderUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();

        return services;
    }

    private static IServiceCollection AddSubscribers(this IServiceCollection services)
    {
        services.AddHostedService<PaymentAcceptedSubscriber>();

        return services;
    }

}