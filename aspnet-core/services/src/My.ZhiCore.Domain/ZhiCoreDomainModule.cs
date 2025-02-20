using My.ZhiCore.FileManagement;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreDomainSharedModule),
        typeof(AbpEmailingModule),
        typeof(BasicManagementDomainModule),
        typeof(DataDictionaryManagementDomainModule),
        typeof(NotificationManagementDomainModule),
        typeof(LanguageManagementDomainModule),
        typeof(FileManagementDomainModule)
    )]
    public class ZhiCoreDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ZhiCoreDomainModule>(); });
        }
    }
}