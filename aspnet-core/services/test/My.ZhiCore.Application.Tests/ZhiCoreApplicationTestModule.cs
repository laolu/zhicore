using Volo.Abp.Modularity;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreApplicationModule),
        typeof(ZhiCoreDomainTestModule)
        )]
    public class ZhiCoreApplicationTestModule : AbpModule
    {

    }
}