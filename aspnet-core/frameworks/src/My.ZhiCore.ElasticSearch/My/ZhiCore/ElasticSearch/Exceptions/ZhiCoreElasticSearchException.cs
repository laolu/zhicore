namespace My.ZhiCore.ElasticSearch.Exceptions;

public class ZhiCoreElasticSearchException : BusinessException
{
    public ZhiCoreElasticSearchException(
        string code = null,
        string message = null,
        string details = null,
        Exception innerException = null,
        LogLevel logLevel = LogLevel.Error)
        : base(
            code,
            message,
            details,
            innerException,
            logLevel
        )
    {
    }
}