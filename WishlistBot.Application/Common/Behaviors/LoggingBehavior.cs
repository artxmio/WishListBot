using MediatR;
using Serilog;
using System.Diagnostics;

namespace WishlistBot.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        Log.Information("Начало обработки запроса: {RequestName} {@Request}", requestName, request);

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var response = await next(cancellationToken);
            stopwatch.Stop();

            Log.Information("Запрос {RequestName} обработан за {ElapsedMilliseconds} мс",
                requestName, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Log.Error(ex, "Ошибка при обработке запроса {RequestName} ({ElapsedMilliseconds} мс)",
                requestName, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
