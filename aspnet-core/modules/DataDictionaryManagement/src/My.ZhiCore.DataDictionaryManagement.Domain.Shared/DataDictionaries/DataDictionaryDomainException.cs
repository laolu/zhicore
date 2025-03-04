namespace My.ZhiCore.DataDictionaryManagement.DataDictionaries
{
    public class DataDictionaryDomainException : BusinessException
    {
        public DataDictionaryDomainException(string code = null, string message = null, string details = null, Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base(code, message, details,
            innerException,
            logLevel
        )
        {
        }
    }
}