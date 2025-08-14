using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WishlistBot.Application.Common.Behaviors;
using WishlistBot.Application.TelegramUpdate.Commands;

namespace WishlistBot.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(provider =>
        {
            provider.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            provider.RegisterServicesFromAssembly(typeof(StartCommandHandler).Assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(LoggingBehavior<,>));

        return services;
    }
}