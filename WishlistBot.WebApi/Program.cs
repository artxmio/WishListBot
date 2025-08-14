using Serilog;
using System.Reflection;
using Telegram.Bot;
using WishlistBot.Application.Common.Mappings;
using WishlistBot.Application.DependencyInjection;
using WishlistBot.Application.Interfaces;
using WishlistBot.Persistanse;
using WishlistBot.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Log
builder.Host.UseSerilog((hostConfig, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(hostConfig.Configuration));

var configuration = builder.Configuration;

//Telegram
builder.Services.AddHttpClient("tg")
    .AddTypedClient<ITelegramBotClient>((httpClient, provider) =>
    {
        var token = configuration["Telegram:Token"]
            ?? throw new InvalidOperationException("Telegram token missing");

        return new TelegramBotClient(token, httpClient);
    });

builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IWishlistBotDbContext).Assembly));
    });

builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddScoped<UpdateRouter>();

////Service to set/delete webhooks
//builder.Services.AddHostedService<ConfigureTelegramWebhook>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<WishlistBotDbContext>();
DbInitializer.Initialize(context);

app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();