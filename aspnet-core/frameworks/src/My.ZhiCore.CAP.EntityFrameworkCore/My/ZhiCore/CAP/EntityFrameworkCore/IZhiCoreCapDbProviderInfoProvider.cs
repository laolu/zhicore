namespace My.ZhiCore.CAP.EntityFrameworkCore;

public interface IZhiCoreCapDbProviderInfoProvider
{
    ZhiCoreCapDbProviderInfo GetOrNull(string dbProviderName);
}