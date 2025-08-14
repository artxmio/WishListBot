using MediatR;
using Serilog;
using Telegram.Bot.Types;
using WishlistBot.Application.TelegramUpdate.Commands;

namespace WishlistBot.WebApi.Services;

public class UpdateRouter(IMediator mediator)
{
    private IMediator _mediator = mediator;

    public async Task RouteAsync(Update update, CancellationToken cancellationToken)
    {
        Log.Debug("Start update routing");

        var user = update.Message?.From ?? update.CallbackQuery?.From;

        Log.Information("Incoming update: {Type}", update.Type);

        switch (update.Type)
        {
            case Telegram.Bot.Types.Enums.UpdateType.Message
                when update.Message?.Text?.StartsWith("/start") == true:

                var command = new StartCommand()
                {
                    UserId = user?.Id ?? 0,
                    UserName = user?.Username ?? "unknown",
                    ChatId = update.Message.Chat.Id,
                };

                try
                {
                    await _mediator.Send(command, cancellationToken);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Ошибка при обработке команды /start");
                }

                Log.Information("New user wa added (@User)", command);
                break;
        }
    }
}