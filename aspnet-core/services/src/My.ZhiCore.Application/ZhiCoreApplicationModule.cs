using My.ZhiCore.FileManagement;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreDomainModule),
        typeof(ZhiCoreApplicationContractsModule),
        typeof(BasicManagementApplicationModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(LanguageManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(FileManagementApplicationModule),
        typeof(ZhiCoreFreeSqlModule)
    )]
    public class ZhiCoreApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ZhiCoreApplicationModule>(); });
        }
    }
}