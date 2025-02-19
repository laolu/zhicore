using System.Collections.Concurrent;
using System.Threading.RateLimiting;

namespace ZhiCore.Application.Common;

public class RateLimitingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private static readonly ConcurrentDictionary<string, TokenBucketRateLimiter> _rateLimiters = new();
    private readonly TokenBucketRateLimiterOptions _options;

    public RateLimitingBehavior()
    {
        _options = new TokenBucketRateLimiterOptions
        {
            TokenLimit = 100,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 50,
            ReplenishmentPeriod = TimeSpan.FromSeconds(1),
            TokensPerPeriod = 10,
            AutoReplenishment = true
        };
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = typeof(TRequest).Name;
        var rateLimiter = _rateLimiters.GetOrAdd(requestType,
            _ => new TokenBucketRateLimiter(_options));

        using var lease = await rateLimiter.AcquireAsync(1, cancellationToken);

        if (!lease.IsAcquired)
        {
            throw new RateLimitExceededException($"Rate limit exceeded for {requestType}");
        }

        return await next();
    }
}

public class RateLimitExceededException : Exception
{
    public RateLimitExceededException(string message) : base(message)
    {
    }
}