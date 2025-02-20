namespace My.ZhiCore.CAP;

public interface IZhiCoreCapTransactionApiFactory
{
    Type TransactionApiType { get; }
    
    ITransactionApi Create(ITransactionApi originalApi);
}