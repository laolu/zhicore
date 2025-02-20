using Volo.Abp;
using Volo.Abp.Modularity;

namespace My.ZhiCore
{

    [DependsOn(typeof(ZhiCoreLocalizationModule))]
    [DependsOn(typeof(AbpTestBaseModule))]
    public class ZhiCoreLocalizationTestBaseModule : AbpModule
    {
    }
}
