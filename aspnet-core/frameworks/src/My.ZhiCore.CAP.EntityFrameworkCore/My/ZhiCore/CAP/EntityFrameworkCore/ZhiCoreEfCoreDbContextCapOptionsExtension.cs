namespace My.ZhiCore.CAP.EntityFrameworkCore;

public class ZhiCoreEfCoreDbContextCapOptionsExtension : ICapOptionsExtension
{
    public string CapUsingDbConnectionString { get; init; }
    
    public void AddServices(IServiceCollection services)
    {
        services.Configure<ZhiCoreEfCoreDbContextCapOptions>(options =>
        {
            options.CapUsingDbConnectionString = CapUsingDbConnectionString;
        });
    }
}