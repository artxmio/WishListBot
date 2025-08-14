using MediatR;
using Serilog;
using Telegram.Bot;
using WishlistBot.Application.Interfaces;

namespace WishlistBot.Application.TelegramUpdate.Commands;

public class StartCommandHandler(IWishlistBotDbContext dbContext, 
    ITelegramBotClient bot)
    : IRequestHandler<StartCommand>
{
    private readonly IWishlistBotDbContext _dbContext = dbContext;
    private readonly ITelegramBotClient _bot = bot;

    public async Task Handle(StartCommand request, CancellationToken cancellationToken)
    {
        Log.Information("StartCommandHandler triggered for user {UserId}", request.UserId);

        //Check
        var isExist = _dbContext.Users.Any(u => u.TelegramUserId == request.UserId);

        if (isExist)
        {
            await _bot.SendMessage(
                chatId: request.ChatId,
                text: $"{request.UserName}! Ты уже зарегистрирован",
                cancellationToken: cancellationToken);

            return;
        }

        //Create new user
        var user = new Domain.User()
        {
            UserId = Guid.NewGuid(),
            TelegramUserId = request.UserId,
            UserName = request.UserName,
            CreatedTime = DateTime.UtcNow,
        };

        try
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _bot.SendMessage(
                chatId: request.ChatId,
                text: $"Привет, {request.UserName}! Добро пожаловать в WishlistBot.\n" +
                      "Ты можешь создать свой первый список желаний с помощью команды /addlist.",
                cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Ошибка при регистрации пользователя {UserId}", request.UserId);
            await _bot.SendMessage(
                chatId: request.ChatId,
                text: "Произошла ошибка при регистрации. Попробуй позже.",
                cancellationToken: cancellationToken);
        }
    }
}