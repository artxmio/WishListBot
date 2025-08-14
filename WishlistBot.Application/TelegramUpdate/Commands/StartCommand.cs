using MediatR;

namespace WishlistBot.Application.TelegramUpdate.Commands;

public class StartCommand : IRequest
{
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public long ChatId { get; set; }
}