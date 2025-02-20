using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace My.ZhiCore.Core
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule))]
    public class ZhiCoreTestBaseModule : AbpModule
    {
    }
}