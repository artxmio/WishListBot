using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WishlistBot.Application.Common.Behaviors;

namespace WishlistBot.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(LoggingBehavior<,>));

        return services;
    }
}