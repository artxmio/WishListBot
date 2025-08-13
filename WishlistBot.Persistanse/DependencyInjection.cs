using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WishlistBot.Application.Interfaces;

namespace WishlistBot.Persistanse;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["Database:ConnectionString"];

        services.AddDbContext<WishlistBotDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<IWishlistBotDbContext>(provider =>
            provider.GetService<WishlistBotDbContext>()
                ?? throw new NullReferenceException("Provider cant't be null"));

        return services;
    }
}
