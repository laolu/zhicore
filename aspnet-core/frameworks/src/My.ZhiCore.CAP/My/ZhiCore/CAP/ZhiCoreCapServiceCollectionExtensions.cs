namespace My.ZhiCore.CAP;

public static class ZhiCoreCapServiceCollectionExtensions
{
    public static ServiceConfigurationContext AddAbpCap(this ServiceConfigurationContext context, Action<CapOptions> capAction)
    {
        context.Services.Replace(ServiceDescriptor.Transient<IUnitOfWork, ZhiCoreCapUnitOfWork>());
        context.Services.Replace(ServiceDescriptor.Transient<UnitOfWork, ZhiCoreCapUnitOfWork>());
        context.Services.AddTransient<ZhiCoreCapUnitOfWork>();
        context.Services.AddCap(capAction);
        return context;
    }
}