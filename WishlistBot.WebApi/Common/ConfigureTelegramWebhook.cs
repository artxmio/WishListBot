using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace WishlistBot.WebApi.Common;

public sealed class ConfigureTelegramWebhook(ITelegramBotClient bot, IConfiguration cfg) : IHostedService
{
    private readonly ITelegramBotClient _bot = bot;
    private readonly IConfiguration _cfg = cfg;

    public async Task StartAsync(CancellationToken ct)
    {
        var url = _cfg["Telegram:WebhookUrl"];

        if (string.IsNullOrWhiteSpace(url))
            throw new InvalidOperationException("Telegram:WebhookUrl is missing");

        await _bot.SetWebhook(
            url: url,
            allowedUpdates: [UpdateType.Message, UpdateType.CallbackQuery],
            //secretToken: secret,
            cancellationToken: ct
        );
    }

    public Task StopAsync(CancellationToken ct)
        => _bot.DeleteWebhook(cancellationToken: ct);
}