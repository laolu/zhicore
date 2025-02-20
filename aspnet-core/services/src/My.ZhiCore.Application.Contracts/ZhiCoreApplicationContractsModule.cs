using My.ZhiCore.FileManagement;

namespace My.ZhiCore
{
    [DependsOn(
        typeof(ZhiCoreDomainSharedModule),
        typeof(AbpObjectExtendingModule),
        typeof(BasicManagementApplicationContractsModule),
        typeof(DataDictionaryManagementApplicationContractsModule),
        typeof(LanguageManagementApplicationContractsModule),
        typeof(NotificationManagementApplicationContractsModule),
        typeof(FileManagementApplicationContractsModule)
    )]
    public class ZhiCoreApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            ZhiCoreDtoExtensions.Configure();
        }
    }
}
