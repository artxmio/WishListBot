using Telegram.Bot;
using Serilog;

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

builder.Services.AddControllers();

////Service to set/delete webhooks
//builder.Services.AddHostedService<ConfigureTelegramWebhook>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();