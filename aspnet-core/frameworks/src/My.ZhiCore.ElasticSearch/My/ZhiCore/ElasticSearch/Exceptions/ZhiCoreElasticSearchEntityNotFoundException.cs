namespace My.ZhiCore.ElasticSearch.Exceptions;

public class ZhiCoreElasticSearchEntityNotFoundException : BusinessException
{
    public ZhiCoreElasticSearchEntityNotFoundException(
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