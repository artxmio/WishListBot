using Microsoft.AspNetCore.Mvc;
using Serilog;
using Telegram.Bot.Types;
using WishlistBot.WebApi.Services;

namespace WishlistBot.WebApi.Controllers;

[ApiController]
[Route("telegram/webhook")]
public class TelegramUpdateController(UpdateRouter updateRouter)
    : Controller
{
    private readonly UpdateRouter _updateRouter = updateRouter;

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] Update update, CancellationToken cancellationToken)
    {
        Log.Information("Webhook received update: {@Update}", update);

        await _updateRouter.RouteAsync(update, cancellationToken);

        Log.Information("Update {UpdateId} processed successfully", update.Id);

        return Ok();
    }
}
