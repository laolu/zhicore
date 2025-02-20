using My.ZhiCore.Cli;
using My.ZhiCore.Cli.Options;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace My.ZhiCore.Core.Cli
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(ZhiCoreCliCoreModule))]
    public class ZhiCoreCoreCliTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            Configure<ZhiCoreCliOptions>(options =>
            {
                options.Owner = "WangJunZzz";
                options.RepositoryId = "abp-vnext-pro";
                options.Token = "abp-vnext-proghp_47vqiabp-vnext-provNkHKJguOJkdHvnxUabp-vnext-protij7Qbdn1Qy3fUabp-vnext-pro";
            });
        }
    }
}