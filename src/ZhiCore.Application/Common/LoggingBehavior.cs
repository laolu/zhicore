using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ZhiCore.Application.Common;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation(
                "Handling {RequestType} request. Request data: {Request}",
                requestType,
                request);

            var response = await next();

            stopwatch.Stop();
            _logger.LogInformation(
                "Handled {RequestType} request. Execution time: {ExecutionTime}ms",
                requestType,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(
                ex,
                "Error handling {RequestType} request. Execution time: {ExecutionTime}ms. Error: {Error}",
                requestType,
                stopwatch.ElapsedMilliseconds,
                ex.Message);
            throw;
        }
    }
}