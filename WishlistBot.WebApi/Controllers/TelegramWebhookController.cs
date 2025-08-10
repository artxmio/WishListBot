using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WishlistBot.WebApi.Controllers;

[ApiController]
[Route("telegram/webhook")]
public class TelegramWebhookController(ITelegramBotClient bot, ILogger<TelegramWebhookController> logger) : ControllerBase
{
    private readonly ITelegramBotClient _bot = bot;
    ILogger<TelegramWebhookController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] Update update,
        CancellationToken cancellationToken)
    {
        // Обрабатываем только то, что нужно
        if (update.Type == UpdateType.Message && update.Message!.Text is string text)
        {
            var chatId = update.Message.Chat.Id;

            if (text.StartsWith("/start", StringComparison.OrdinalIgnoreCase))
            {
                await _bot.SendMessage(
                    chatId: chatId,
                    text: "Привет! Я ASP.NET Core бот на вебхуке. Скажи что-нибудь.",
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                await _bot.SendMessage(
                    chatId: chatId,
                    text: $"Эхо: {text}",
                    replyParameters: update.Message.MessageId,
                    cancellationToken: cancellationToken
                );
            }
        }
        else if (update.Type == UpdateType.CallbackQuery)
        {
            // Обработка нажатий инлайн-кнопок
            var cq = update.CallbackQuery!;
            await _bot.AnswerCallbackQuery(cq.Id, cancellationToken: cancellationToken);
            await _bot.SendMessage(cq.Message!.Chat.Id, $"Нажато: {cq.Data}", cancellationToken: cancellationToken);
        }

        return Ok(update);
    }
}
